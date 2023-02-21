using EventBus.Events;

namespace FlightsAvailability.Search.ResultsProcessor.Skyscanner.IntegrationEvents.Events
{
    public record SkyscannerResponseReceivedEvent : RawIntegrationEvent
    {
        public Guid ParentEventId { get; set; }
        public string QueryKey { get; set; }
        public int PollNumber { get; set; }
    }
}
