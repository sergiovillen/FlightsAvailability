using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.Entities;
using FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.Events;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
using System.Xml.Linq;

namespace FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.EventHandling
{
    public class FlightSearchQueryReceivedEventHandler : IIntegrationEventHandler<FlightSearchQueryReceivedEvent>
    {
        private const string DAPR_BINDING_SKYSCANNER_SEARCH_CREATE = "skyscanner-search-create";
        private const string DAPR_BINDING_SKYSCANNER_SEARCH_POLL = "skyscanner-search-poll";
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
            metadata.Add("X-api-key", "prtl6749387986743898559646983194");
            var data = new { query = 
                new {
                    market = "ES",
		            locale = "es-ES",
		            currency = "EUR",
                    adults = 1,
		            cabin_class = "CABIN_CLASS_ECONOMY",
                    query_legs = new List<object>()
                    {
                        new { 
                            origin_place_id = new { iata = "MAD"},
                            destination_place_id = new { iata = "CDG"},
                            date = new { year = 2023, month = 6,day = 22 }
                            }
                    }
                } 
            };
            var searchCreateResponse = await _daprClient.InvokeBindingAsync<object, dynamic>(DAPR_BINDING_SKYSCANNER_SEARCH_CREATE, "post", data, (IReadOnlyDictionary<string, string>) metadata);

            var response = JsonSerializer.Deserialize<SkyscannerResponse>(searchCreateResponse.ToString())!;
            if (response != null && response.status == "RESULT_STATUS_INCOMPLETE")
            {
                metadata.Add("path", response.sessionToken);
                var responseStatus = response.status;
                while (responseStatus == "RESULT_STATUS_INCOMPLETE")
                {
                    var searchPollResponse = await _daprClient.InvokeBindingAsync<object, dynamic>(DAPR_BINDING_SKYSCANNER_SEARCH_POLL, "post", string.Empty, (IReadOnlyDictionary<string, string>)metadata);
                    var pollResponse = JsonSerializer.Deserialize<SkyscannerResponse>(searchPollResponse.ToString())!;
                    responseStatus = pollResponse.status;
                }
            }
            //var request = new BindingRequest(DAPR_BINDING_SKYSCANNER_SEARCH_CREATE, "post");
            //request.Metadata.Add("X-api-key", "prtl6749387986743898559646983194");
            //request.Data = data;
            //var response = await _daprClient.InvokeBindingAsync(request);

            await Task.Delay(3000);
        }
    }
}
