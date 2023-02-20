using EventBus.Events;
using FlightsAvailability.Search.ResultsProcessor.Skyscanner.IntegrationEvents.BindingResponses;

namespace FlightsAvailability.Search.ResultsProcessor.Skyscanner.IntegrationEvents.Events
{
    public record SkyscannerResponseReceivedEvent : RawIntegrationEvent
    {
        public Guid ParentEventId { get; set; }
        public string QueryKey { get; set; }
        public int Retry { get; set; }
    }
}
