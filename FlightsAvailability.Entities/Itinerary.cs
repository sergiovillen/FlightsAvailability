namespace FlightsAvailability.Entities
{
    public record Itinerary
    {
        public string? SearchProvider { get; set; }
        public List<Segment>? Segments { get; set; }
        public int DurationInMinutes { get; set; }

        public Price? Price { get; set; }
    }
}