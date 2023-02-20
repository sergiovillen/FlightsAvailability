namespace FlightsAvailability.Search.ResultsProcessor.Skyscanner.IntegrationEvents.BindingResponses
{
    public class Segment
    {
        public string originPlaceId { get; set; }
        public string destinationPlaceId { get; set; }
        public DepartureDateTime departureDateTime { get; set; }
        public ArrivalDateTime arrivalDateTime { get; set; }
        public int durationInMinutes { get; set; }
        public string marketingFlightNumber { get; set; }
        public string marketingCarrierId { get; set; }
        public string operatingCarrierId { get; set; }
    }

    public class Itinerary
    {
        public List<PricingOption> pricingOptions { get; set; }
        public List<string> legIds { get; set; }
        public object sustainabilityData { get; set; }
    }

    public class Leg
    {
        public string originPlaceId { get; set; }
        public string destinationPlaceId { get; set; }
        public DepartureDateTime departureDateTime { get; set; }
        public ArrivalDateTime arrivalDateTime { get; set; }
        public int durationInMinutes { get; set; }
        public int stopCount { get; set; }
        public List<string> marketingCarrierIds { get; set; }
        public List<string> operatingCarrierIds { get; set; }
        public List<string> segmentIds { get; set; }
    }

    public class Place
    {
        public string entityId { get; set; }
        public string parentId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string iata { get; set; }
        public object coordinates { get; set; }
    }

    public class Alliance
    {
        public string name { get; set; }
    }

    public class Carrier
    {
        public string name { get; set; }
        public string allianceId { get; set; }
        public string imageUrl { get; set; }
        public string iata { get; set; }
    }

    public class Agent
    {
        public string name { get; set; }
        public string type { get; set; }
        public string imageUrl { get; set; }
        public int feedbackCount { get; set; }
        public double rating { get; set; }
        public RatingBreakdown ratingBreakdown { get; set; }
        public bool isOptimisedForMobile { get; set; }
    }

    public class ArrivalDateTime
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
    }

    public class Best
    {
        public double score { get; set; }
        public string itineraryId { get; set; }
    }

    public class Cheapest
    {
        public double score { get; set; }
        public string itineraryId { get; set; }
    }

    public class Content
    {
        public Results results { get; set; }
        public Stats stats { get; set; }
        public SortingOptions sortingOptions { get; set; }
    }

    public class DepartureDateTime
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
    }

    public class Direct
    {
        public Total total { get; set; }
        public TicketTypes ticketTypes { get; set; }
    }

    public class Fare
    {
        public string segmentId { get; set; }
        public string bookingCode { get; set; }
        public string fareBasisCode { get; set; }
    }

    public class Fastest
    {
        public double score { get; set; }
        public string itineraryId { get; set; }
    }

    public class Item
    {
        public Price price { get; set; }
        public string agentId { get; set; }
        public string deepLink { get; set; }
        public List<Fare> fares { get; set; }
    }

    public class Itineraries
    {
        public int minDuration { get; set; }
        public int maxDuration { get; set; }
        public Total total { get; set; }
        public Stops stops { get; set; }
        public bool hasChangeAirportTransfer { get; set; }
    }


    public class MinPrice
    {
        public string amount { get; set; }
        public string unit { get; set; }
        public string updateStatus { get; set; }
    }

    public class MultiTicketNonNpt
    {
        public int count { get; set; }
        public MinPrice minPrice { get; set; }
    }

    public class MultiTicketNpt
    {
        public int count { get; set; }
        public MinPrice minPrice { get; set; }
    }

    public class OneStop
    {
        public Total total { get; set; }
        public TicketTypes ticketTypes { get; set; }
    }

    public class Price
    {
        public string amount { get; set; }
        public string unit { get; set; }
        public string updateStatus { get; set; }
    }

    public class PricingOption
    {
        public Price price { get; set; }
        public List<string> agentIds { get; set; }
        public List<Item> items { get; set; }
        public string transferType { get; set; }
        public string id { get; set; }
    }

    public class RatingBreakdown
    {
        public double customerService { get; set; }
        public double reliablePrices { get; set; }
        public double clearExtraFees { get; set; }
        public double easeOfBooking { get; set; }
        public double other { get; set; }
    }

    public class Results
    {
        public Dictionary<string, Itinerary> itineraries { get; set; }
        public Dictionary<string, Leg> legs { get; set; }
        public Dictionary<string, Segment> segments { get; set; }
        public Dictionary<string, Place> places { get; set; }
        public Dictionary<string, Carrier> carriers { get; set; }
        public Dictionary<string, Agent> agents { get; set; }
        public Dictionary<string, Alliance> alliances { get; set; }
    }

    public class SkyscannerResponse
    {
        public string sessionToken { get; set; }
        public string status { get; set; }
        public string action { get; set; }
        public Content content { get; set; }
    }

    public class SingleTicket
    {
        public int count { get; set; }
        public MinPrice minPrice { get; set; }
    }

    public class SortingOptions
    {
        public List<Best> best { get; set; }
        public List<Cheapest> cheapest { get; set; }
        public List<Fastest> fastest { get; set; }
    }

    public class Stats
    {
        public Itineraries itineraries { get; set; }
    }

    public class Stops
    {
        public Direct direct { get; set; }
        public OneStop oneStop { get; set; }
        public TwoPlusStops twoPlusStops { get; set; }
    }

    public class TicketTypes
    {
        public SingleTicket singleTicket { get; set; }
        public MultiTicketNonNpt multiTicketNonNpt { get; set; }
        public MultiTicketNpt multiTicketNpt { get; set; }
    }

    public class Total
    {
        public int count { get; set; }
        public MinPrice minPrice { get; set; }
    }

    public class TwoPlusStops
    {
        public Total total { get; set; }
        public TicketTypes ticketTypes { get; set; }
    }
}
