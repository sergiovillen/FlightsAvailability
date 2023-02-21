using EventBus.Events;
using FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.BindingResponses;

namespace FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.Events
{
    public record SkyscannerResponseReceivedEvent : RawIntegrationEvent
    {
        public Guid ParentEventId { get; set; }
        public string QueryKey { get; set; }
        public int PollNumber { get; set; }
    }
}
