namespace FlightsAvailability.Shared
{
    public class FlightSearchRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime? Time { get; set; }
    }
}
