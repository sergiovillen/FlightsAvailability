using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.BindingResponses;
using FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.Events;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Xml.Linq;

namespace FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.EventHandling
{
    public class FlightSearchQueryReceivedEventHandler : IIntegrationEventHandler<FlightSearchQueryReceivedEvent>
    {
        private const string DAPR_BINDING_SKYSCANNER_SEARCH_CREATE = "skyscanner-search-create";
        private const string DAPR_BINDING_SKYSCANNER_SEARCH_POLL = "skyscanner-search-poll";
        private const string SKYSCANNER_API_KEY = "prtl6749387986743898559646983194";
        private const string SKYSCANNER_API_KEY_HEADER_NAME = "X-api-key";
        private const string SKYSCANNER_PARAM_MARKET = "ES";
        private const string SKYSCANNER_PARAM_LOCALE = "es-ES";
        private const string SKYSCANNER_PARAM_CURRENCY = "EUR";
        private const int SKYSCANNER_PARAM_ADULTS = 1;
        private const string SKYSCANNER_PARAM_CABIN_CLASS = "CABIN_CLASS_ECONOMY";
        private const string SKYSCANNER_RESULT_STATUS_INCOMPLETE = "RESULT_STATUS_INCOMPLETE";
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;
        private readonly DaprClient _daprClient;

        public FlightSearchQueryReceivedEventHandler(            
            IEventBus eventBus,
            ILogger<FlightSearchQueryReceivedEventHandler> logger,
            DaprClient daprClient)
        {            
            _eventBus = eventBus;
            _logger = logger;
            _daprClient = daprClient;
        }
        public async Task Handle(FlightSearchQueryReceivedEvent @event)
        {            
            var metadata = new Dictionary<string, string>();
            metadata.Add(SKYSCANNER_API_KEY_HEADER_NAME, SKYSCANNER_API_KEY);
            var data = new { query = 
                new {
                    market = SKYSCANNER_PARAM_MARKET,
		            locale = SKYSCANNER_PARAM_LOCALE,
		            currency = SKYSCANNER_PARAM_CURRENCY,
                    adults = SKYSCANNER_PARAM_ADULTS,
		            cabin_class = SKYSCANNER_PARAM_CABIN_CLASS,
                    query_legs = new List<object>()
                    {
                        new { 
                            origin_place_id = new { iata = @event.From},
                            destination_place_id = new { iata = @event.To},
                            date = new { year = @event.Time.Value.Year, month = @event.Time.Value.Month,day = @event.Time.Value.Day }
                            }
                    }
                } 
            };
            var searchCreateResponse = await _daprClient.InvokeBindingAsync<object, dynamic>(DAPR_BINDING_SKYSCANNER_SEARCH_CREATE, "post", data, (IReadOnlyDictionary<string, string>) metadata);

            var response = JsonSerializer.Deserialize<SkyscannerResponse>(searchCreateResponse.ToString())!;
            int retry = 1;
            await _eventBus.PublishAsync(new SkyscannerResponseReceivedEvent() {
                ParentEventId = @event.Id,
                QueryKey = @event.QueryKey,
                Retry = retry,
                RawData = searchCreateResponse.ToString()
            });
            if (response != null && response.status == SKYSCANNER_RESULT_STATUS_INCOMPLETE)
            {
                metadata.Add("path", response.sessionToken);
                var responseStatus = response.status;
                while (responseStatus == SKYSCANNER_RESULT_STATUS_INCOMPLETE && retry < 3)
                {
                    try
                    {
                        retry++;
                        var searchPollResponse = await _daprClient.InvokeBindingAsync<object, dynamic>(DAPR_BINDING_SKYSCANNER_SEARCH_POLL, "post", string.Empty, (IReadOnlyDictionary<string, string>)metadata);
                        var pollResponse = JsonSerializer.Deserialize<SkyscannerResponse>(searchPollResponse.ToString())!;
                        await _eventBus.PublishAsync(new SkyscannerResponseReceivedEvent()
                        {
                            ParentEventId = @event.Id,
                            QueryKey = @event.QueryKey,
                            Retry = retry,
                            RawData = pollResponse.ToString()
                        });
                        responseStatus = pollResponse.status;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex,"Error when invoking skyscanner");
                    }
                }
            }
        }
    }
}
