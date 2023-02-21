using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Entities.Events;
using System.Text.Json;

namespace FlightsAvailability.Search.Personalization.Service.IntegrationEvents.EventHandling
{
    public class FlightSearchResultsProcessedEventHandler : IIntegrationEventHandler<FlightSearchResultsProcessedEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;
        private readonly DaprClient _daprClient;

        public FlightSearchResultsProcessedEventHandler(
            IEventBus eventBus,
            ILogger<FlightSearchResultsProcessedEventHandler> logger,
            DaprClient daprClient)
        {
            _eventBus = eventBus;
            _logger = logger;
            _daprClient = daprClient;
        }
        public async Task Handle(FlightSearchResultsProcessedEvent @event)
        {
            

        }
    }
}
