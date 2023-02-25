using FlightsAvailability.Entities;

namespace FlightsAvailability.Search.Personalization.Service.Models
{
    public record FlightSearchResults
    {
        public string QueryKey { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<Itinerary>? Itineraries { get; set; }
    }
}
