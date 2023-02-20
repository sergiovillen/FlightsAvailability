namespace FlightsAvailability.Entities
{
    public class Itinerary
    {
        public List<Segment> Segments { get; set; }
        public int DurationInMinutes { get; set; }

        public Price Price { get; set; }
    }
}