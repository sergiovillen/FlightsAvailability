using FlightsAvailability.Entities;

namespace FlightsAvailability.Search.Personalization.Service.Models
{
    public record FlightSearchResults
    {
        public string QueryKey { get; set; }
        public List<Itinerary>? Itineraries { get; set; }
    }
}
