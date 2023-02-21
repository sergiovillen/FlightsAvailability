using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Entities.Events;
using FlightsAvailability.Search.Agent.Skyscanner.Contracts;
using FlightsAvailability.Search.ResultsProcessor.Skyscanner.IntegrationEvents.Events;
using System.Text.Json;
using static Grpc.Core.Metadata;

namespace FlightsAvailability.Search.ResultsProcessor.Skyscanner.IntegrationEvents.EventHandling
{
    public class SkyscannerResponseReceivedEventHandler : IIntegrationEventHandler<SkyscannerResponseReceivedEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;
        private readonly DaprClient _daprClient;

        public SkyscannerResponseReceivedEventHandler(
            IEventBus eventBus,
            ILogger<SkyscannerResponseReceivedEventHandler> logger,
            DaprClient daprClient)
        {
            _eventBus = eventBus;
            _logger = logger;
            _daprClient = daprClient;
        }
        public async Task Handle(SkyscannerResponseReceivedEvent @event)
        {
            var pollResponse = JsonSerializer.Deserialize<SkyscannerResponse>(@event.RawData)!;
            var results = new FlightSearchResultsProcessedEvent()
            {
                SearchProvider = "Skyscanner",
                ParentEventId = @event.Id,
                QueryKey = @event.QueryKey,
                Itineraries = new List<Entities.Itinerary>()
            };
            foreach(var rawItinerary in pollResponse.content.results.itineraries)
            {
                var itinerary = new Entities.Itinerary() { Segments = new List<Entities.Segment>() };
                var legId = rawItinerary.Value.legIds[0];
                var leg = pollResponse.content.results.legs[legId];
                itinerary.DurationInMinutes = leg.durationInMinutes;
                foreach(var rawSegmentId in leg.segmentIds)
                {
                    var rawSegment = pollResponse.content.results.segments[rawSegmentId];
                    var segment = new Entities.Segment();
                    segment.DurationInMinutes = rawSegment.durationInMinutes;
                    segment.ArrivalTime = new Entities.Time()
                    {
                        Year = rawSegment.arrivalDateTime.year,
                        Month = rawSegment.arrivalDateTime.month,
                        Day = rawSegment.arrivalDateTime.day,
                        Hour = rawSegment.arrivalDateTime.hour,
                        Minute = rawSegment.arrivalDateTime.minute,
                        Second = rawSegment.arrivalDateTime.second
                    };
                    segment.DepartureTime = new Entities.Time()
                    {
                        Year = rawSegment.departureDateTime.year,
                        Month = rawSegment.departureDateTime.month,
                        Day = rawSegment.departureDateTime.day,
                        Hour = rawSegment.departureDateTime.hour,
                        Minute = rawSegment.departureDateTime.minute,
                        Second = rawSegment.departureDateTime.second
                    };
                    segment.Arrival = new Entities.Place()
                    {
                        Iata = pollResponse.content.results.places[rawSegment.destinationPlaceId].iata
                    };
                    segment.Departure = new Entities.Place()
                    {
                        Iata = pollResponse.content.results.places[rawSegment.originPlaceId].iata
                    };
                    segment.Number = rawSegment.marketingFlightNumber;
                    segment.CarrierCode = pollResponse.content.results.carriers[rawSegment.marketingCarrierId].iata;
                    itinerary.Segments.Add(segment);
                }

                results.Itineraries.Add(itinerary);
            }
            await _eventBus.PublishAsync(results);

        }
    }
}
