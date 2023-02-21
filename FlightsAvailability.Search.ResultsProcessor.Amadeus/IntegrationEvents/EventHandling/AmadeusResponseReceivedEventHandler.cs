using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Search.ResultsProcessor.Amadeus.IntegrationEvents.BindingResponses;
using FlightsAvailability.Search.ResultsProcessor.Amadeus.IntegrationEvents.Events;
using Google.Type;
using System.Text.Json;
using System.Xml;

namespace FlightsAvailability.Search.ResultsProcessor.Amadeus.IntegrationEvents.EventHandling
{
    public class AmadeusResponseReceivedEventHandler : IIntegrationEventHandler<AmadeusResponseReceivedEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;
        private readonly DaprClient _daprClient;

        public AmadeusResponseReceivedEventHandler(
            IEventBus eventBus,
            ILogger<AmadeusResponseReceivedEventHandler> logger,
            DaprClient daprClient)
        {
            _eventBus = eventBus;
            _logger = logger;
            _daprClient = daprClient;
        }
        public async Task Handle(AmadeusResponseReceivedEvent @event)
        {
            var response = JsonSerializer.Deserialize<AmadeusResponse>(@event.RawData.ToString())!;
            var results = new FlightSearchResultsProcessedEvent()
            {
                ParentEventId = @event.Id,
                QueryKey = @event.QueryKey,
                Itineraries = new List<Entities.Itinerary>()
            };
            foreach(var rawFlightOffer in response.data)
            {
                foreach(var rawItinerary in rawFlightOffer.itineraries)
                {
                    var itinerary = new Entities.Itinerary();
                    itinerary.DurationInMinutes = (int)XmlConvert.ToTimeSpan(rawItinerary.duration).TotalMinutes;
                    itinerary.Price = new Entities.Price() { Amount = double.Parse(rawFlightOffer.price.grandTotal), Currency = rawFlightOffer.price.currency };
                    itinerary.Segments = new List<Entities.Segment>();
                    foreach(var rawSegment in rawItinerary.segments)
                    {
                        var segment = new Entities.Segment();
                        segment.Departure = new Entities.Place() { Iata = rawSegment.departure.iataCode };
                        segment.Arrival = new Entities.Place() { Iata = rawSegment.arrival.iataCode };
                        segment.DepartureTime = new Entities.Time() { 
                            Year = rawSegment.departure.at.Year,
                            Month = rawSegment.departure.at.Month,
                            Day = rawSegment.departure.at.Day,
                            Hour = rawSegment.departure.at.Hour,
                            Minute = rawSegment.departure.at.Minute,
                            Second = rawSegment.departure.at.Second,
                        };
                        segment.ArrivalTime = new Entities.Time()
                        {
                            Year = rawSegment.arrival.at.Year,
                            Month = rawSegment.arrival.at.Month,
                            Day = rawSegment.arrival.at.Day,
                            Hour = rawSegment.arrival.at.Hour,
                            Minute = rawSegment.arrival.at.Minute,
                            Second = rawSegment.arrival.at.Second,
                        };
                        segment.CarrierCode = rawSegment.carrierCode;
                        segment.Number = rawSegment.number;
                        itinerary.Segments.Add(segment);
                    }
                    results.Itineraries.Add(itinerary);
                }
            }
            await _eventBus.PublishAsync(results);
        }
    }
}
