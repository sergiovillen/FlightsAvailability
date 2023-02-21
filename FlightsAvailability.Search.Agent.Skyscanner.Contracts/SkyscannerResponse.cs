namespace FlightsAvailability.Search.Agent.Skyscanner.Contracts
{
    public record Segment
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

    public record Itinerary
    {
        public List<PricingOption> pricingOptions { get; set; }
        public List<string> legIds { get; set; }
        public object sustainabilityData { get; set; }
    }

    public record Leg
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

    public record Place
    {
        public string entityId { get; set; }
        public string parentId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string iata { get; set; }
        public object coordinates { get; set; }
    }

    public record Alliance
    {
        public string name { get; set; }
    }

    public record Carrier
    {
        public string name { get; set; }
        public string allianceId { get; set; }
        public string imageUrl { get; set; }
        public string iata { get; set; }
    }

    public record Agent
    {
        public string name { get; set; }
        public string type { get; set; }
        public string imageUrl { get; set; }
        public int feedbackCount { get; set; }
        public double rating { get; set; }
        public RatingBreakdown ratingBreakdown { get; set; }
        public bool isOptimisedForMobile { get; set; }
    }

    public record ArrivalDateTime
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
    }

    public record Best
    {
        public double score { get; set; }
        public string itineraryId { get; set; }
    }

    public record Cheapest
    {
        public double score { get; set; }
        public string itineraryId { get; set; }
    }

    public record Content
    {
        public Results results { get; set; }
        public Stats stats { get; set; }
        public SortingOptions sortingOptions { get; set; }
    }

    public record DepartureDateTime
    {
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
    }

    public record Direct
    {
        public Total total { get; set; }
        public TicketTypes ticketTypes { get; set; }
    }

    public record Fare
    {
        public string segmentId { get; set; }
        public string bookingCode { get; set; }
        public string fareBasisCode { get; set; }
    }

    public record Fastest
    {
        public double score { get; set; }
        public string itineraryId { get; set; }
    }

    public record Item
    {
        public Price price { get; set; }
        public string agentId { get; set; }
        public string deepLink { get; set; }
        public List<Fare> fares { get; set; }
    }

    public record Itineraries
    {
        public int minDuration { get; set; }
        public int maxDuration { get; set; }
        public Total total { get; set; }
        public Stops stops { get; set; }
        public bool hasChangeAirportTransfer { get; set; }
    }


    public record MinPrice
    {
        public string amount { get; set; }
        public string unit { get; set; }
        public string updateStatus { get; set; }
    }

    public record MultiTicketNonNpt
    {
        public int count { get; set; }
        public MinPrice minPrice { get; set; }
    }

    public record MultiTicketNpt
    {
        public int count { get; set; }
        public MinPrice minPrice { get; set; }
    }

    public record OneStop
    {
        public Total total { get; set; }
        public TicketTypes ticketTypes { get; set; }
    }

    public record Price
    {
        public string amount { get; set; }
        public string unit { get; set; }
        public string updateStatus { get; set; }
    }

    public record PricingOption
    {
        public Price price { get; set; }
        public List<string> agentIds { get; set; }
        public List<Item> items { get; set; }
        public string transferType { get; set; }
        public string id { get; set; }
    }

    public record RatingBreakdown
    {
        public double customerService { get; set; }
        public double reliablePrices { get; set; }
        public double clearExtraFees { get; set; }
        public double easeOfBooking { get; set; }
        public double other { get; set; }
    }

    public record Results
    {
        public Dictionary<string, Itinerary> itineraries { get; set; }
        public Dictionary<string, Leg> legs { get; set; }
        public Dictionary<string, Segment> segments { get; set; }
        public Dictionary<string, Place> places { get; set; }
        public Dictionary<string, Carrier> carriers { get; set; }
        public Dictionary<string, Agent> agents { get; set; }
        public Dictionary<string, Alliance> alliances { get; set; }
    }

    public record SkyscannerResponse
    {
        public string sessionToken { get; set; }
        public string status { get; set; }
        public string action { get; set; }
        public Content content { get; set; }
    }

    public record SingleTicket
    {
        public int count { get; set; }
        public MinPrice minPrice { get; set; }
    }

    public record SortingOptions
    {
        public List<Best> best { get; set; }
        public List<Cheapest> cheapest { get; set; }
        public List<Fastest> fastest { get; set; }
    }

    public record Stats
    {
        public Itineraries itineraries { get; set; }
    }

    public record Stops
    {
        public Direct direct { get; set; }
        public OneStop oneStop { get; set; }
        public TwoPlusStops twoPlusStops { get; set; }
    }

    public record TicketTypes
    {
        public SingleTicket singleTicket { get; set; }
        public MultiTicketNonNpt multiTicketNonNpt { get; set; }
        public MultiTicketNpt multiTicketNpt { get; set; }
    }

    public record Total
    {
        public int count { get; set; }
        public MinPrice minPrice { get; set; }
    }

    public record TwoPlusStops
    {
        public Total total { get; set; }
        public TicketTypes ticketTypes { get; set; }
    }
}