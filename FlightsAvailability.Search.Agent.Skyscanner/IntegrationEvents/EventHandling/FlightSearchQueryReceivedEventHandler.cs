using EventBus.Abstractions;
using FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.Events;

namespace FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.EventHandling
{
    public class FlightSearchQueryReceivedEventHandler : IIntegrationEventHandler<FlightSearchQueryReceivedEvent>
    {        
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;

        public FlightSearchQueryReceivedEventHandler(            
            IEventBus eventBus,
            ILogger<FlightSearchQueryReceivedEventHandler> logger)
        {            
            _eventBus = eventBus;
            _logger = logger;
        }
        public async Task Handle(FlightSearchQueryReceivedEvent @event)
        {
            await Task.Delay(3000);
        }
    }
}
