using EventBus.Events;

namespace FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.Entities
{
    public record SkyscannerResponseReceivedEvent : IntegrationEvent
    {
        public Guid ParentEventId { get; set; }
        public string QueryKey { get; set; }
        public int Retry { get; set; }
        public SkyscannerResponse Response { get; set; }
    }
}
