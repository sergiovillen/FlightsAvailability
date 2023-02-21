using EventBus.Events;
using FlightsAvailability.Entities;

namespace FlightsAvailability.Search.ResultsProcessor.Skyscanner.IntegrationEvents.Events
{
    public record FlightSearchResultsProcessedEvent : IntegrationEvent
    {
        public Guid ParentEventId { get; set; }
        public string QueryKey { get; set; }
        public List<Itinerary>? Itineraries { get; set; }
    }
}
