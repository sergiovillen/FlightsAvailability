using EventBus.Events;

namespace FlightsAvailability.Entities.Events
{
    public record FlightSearchResultsProcessedEvent : IntegrationEvent
    {
        public string? SearchProvider { get; set; }
        public Guid ParentEventId { get; set; }
        public string? QueryKey { get; set; }
        public List<Itinerary>? Itineraries { get; set; }
    }
}
