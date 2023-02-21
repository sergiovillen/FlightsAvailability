namespace FlightsAvailability.Entities
{
    public record Price
    {
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}