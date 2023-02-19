using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.Events;
using System.Text.Json;

namespace FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.EventHandling
{
    public class FlightSearchQueryReceivedEventHandler : IIntegrationEventHandler<FlightSearchQueryReceivedEvent>
    {
        private const string SECRET_STORE_NAME = "flights-availability-secretstore";
        private const string AMADEUS_API_CLIENT_CREDENTIALS_SECRET_NAME = "amaudeus-api-client-credentials";
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
            var clientId = await _daprClient.GetSecretAsync(SECRET_STORE_NAME, AMADEUS_API_CLIENT_CREDENTIALS_SECRET_NAME);
        }
    }
}
