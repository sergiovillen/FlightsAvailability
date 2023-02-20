using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Search.ResultsProcessor.Skyscanner.IntegrationEvents.Events;

namespace FlightsAvailability.Search.ResultsProcessor.Skyscanner.IntegrationEvents.EventHandling
{
    public class SkyscannerResponseReceivedEventHandler : IIntegrationEventHandler<SkyscannerResponseReceivedEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;
        private readonly DaprClient _daprClient;

        public SkyscannerResponseReceivedEventHandler(
            IEventBus eventBus,
            ILogger<SkyscannerResponseReceivedEventHandler> logger,
            DaprClient daprClient)
        {
            _eventBus = eventBus;
            _logger = logger;
            _daprClient = daprClient;
        }
        public Task Handle(SkyscannerResponseReceivedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
