using EventBus.Events;

namespace FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.Events
{
    public record AmadeusResponseReceivedEvent : RawIntegrationEvent
    {
        public Guid ParentEventId { get; set; }
    }
}
