using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Search.ResultsProcessor.Amadeus.IntegrationEvents.Events;

namespace FlightsAvailability.Search.ResultsProcessor.Amadeus.IntegrationEvents.EventHandling
{
    public class AmadeusResponseReceivedEventHandler : IIntegrationEventHandler<AmadeusResponseReceivedEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;
        private readonly DaprClient _daprClient;

        public AmadeusResponseReceivedEventHandler(
            IEventBus eventBus,
            ILogger<AmadeusResponseReceivedEventHandler> logger,
            DaprClient daprClient)
        {
            _eventBus = eventBus;
            _logger = logger;
            _daprClient = daprClient;
        }
        public Task Handle(AmadeusResponseReceivedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
