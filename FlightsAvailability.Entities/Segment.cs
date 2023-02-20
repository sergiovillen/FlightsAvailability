namespace FlightsAvailability.Entities
{
    public class Segment
    {
        public Place Departure { get; set; }
        public Place Arrival { get; set; }

        public Time DepartureTime { get; set; }

        public Time ArrivalTime { get; set; }

        public string CarrierCode { get; set; }

        public string Number { get; set; }
        public int DurationInMinutes { get; set; }

    }
}