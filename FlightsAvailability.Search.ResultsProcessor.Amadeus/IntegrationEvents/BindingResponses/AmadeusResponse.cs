using System;

namespace FlightsAvailability.Search.ResultsProcessor.Amadeus.IntegrationEvents.BindingResponses
{
    public record AmadeusResponse
    {
        public Meta meta { get; set; }
        public List<FlightOffer> data { get; set; }
        public Dictionaries dictionaries { get; set; }
    }

    public record Dictionaries
    {
        public Dictionary<string, Location> locations { get; set; }
        public Dictionary<string, string> aircraft { get; set; }
        public Dictionary<string, string> currencies { get; set; }
        public Dictionary<string, string> carriers { get; set; }
    }

    public record Location
    {
        public string cityCode { get; set; }
        public string countryCode { get; set; }
    }

    public record FlightOffer
    {
        public string type { get; set; }
        public string id { get; set; }
        public string source { get; set; }
        public bool instantTicketingRequired { get; set; }
        public bool nonHomogeneous { get; set; }
        public bool oneWay { get; set; }
        public string lastTicketingDate { get; set; }
        public int numberOfBookableSeats { get; set; }
        public List<Itinerary> itineraries { get; set; }
        public Price price { get; set; }
        public PricingOptions pricingOptions { get; set; }
        public List<string> validatingAirlineCodes { get; set; }
        public List<TravelerPricing> travelerPricings { get; set; }
    }

    public record TravelerPricing
    {
        public string travelerId { get; set; }
        public string fareOption { get; set; }
        public string travelerType { get; set; }
        public Price price { get; set; }
        public List<FareDetailsBySegment> fareDetailsBySegment { get; set; }
    }

    public record FareDetailsBySegment
    {
        public string segmentId { get; set; }
        public string cabin { get; set; }
        public string fareBasis { get; set; }
        public string brandedFare { get; set; }
        public string @class { get; set; }
        public IncludedCheckedBags includedCheckedBags { get; set; }
    }

    public record IncludedCheckedBags
    {
        public int quantity { get; set; }
    }

    public record PricingOptions
    {
        public List<string> fareType { get; set; }
        public bool includedCheckedBagsOnly { get; set; }
    }

    public record Price
    {
        public string currency { get; set; }
        public string total { get; set; }
        public string @base { get; set; }
        public List<Fee> fees { get; set; }
        public string grandTotal { get; set; }
        public List<AdditionalService> additionalServices { get; set; }
    }

    public record AdditionalService
    {
        public string amount { get; set; }
        public string type { get; set; }
    }

    public record Fee
    {
        public string amount { get; set; }
        public string type { get; set; }
    }

    public record Itinerary
    {
        public string duration { get; set; }
        public List<Segment> segments { get; set; }
    }

    public record Segment
    {
        public Departure departure { get; set; }
        public Arrival arrival { get; set; }
        public string carrierCode { get; set; }
        public string number { get; set; }
        public Aircraft aircraft { get; set; }
        public Operating operating { get; set; }
        public string duration { get; set; }
        public string id { get; set; }
        public int numberOfStops { get; set; }
        public bool blacklistedInEU { get; set; }
    }

    public record Operating
    {
        public string carrierCode { get; set; }
    }

    public record Aircraft
    { 
        public string code { get; set; }
    }

        public record Departure
    {
        public string iataCode { get; set; }
        public string terminal { get; set; }
        public DateTime at { get; set; }
    }

    public record Arrival
    {
        public string iataCode { get; set; }
        public string terminal { get; set; }
        public DateTime at { get; set; }
    }

    public record Meta
    {
        public int count { get; set; }
        public Links links { get; set; }
    }

    public record Links
    {
        public string self { get; set; }
    }
}
