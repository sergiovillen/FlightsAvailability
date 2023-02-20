using EventBus.Events;

namespace FlightsAvailability.Search.ResultsProcessor.Amadeus.IntegrationEvents.Events
{
    public record AmadeusResponseReceivedEvent : RawIntegrationEvent
    {
        public Guid ParentEventId { get; set; }
        public string QueryKey { get; set; }
    }
}
