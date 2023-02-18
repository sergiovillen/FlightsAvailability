using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.Events;
using System.Text.Json;

namespace FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.EventHandling
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
            
         
        }
    }
}
