using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Entities.Events;
using FlightsAvailability.Search.Personalization.Service.Repositories;
using System.Text.Json;

namespace FlightsAvailability.Search.Personalization.Service.IntegrationEvents.EventHandling
{
    public class FlightSearchResultsProcessedEventHandler : IIntegrationEventHandler<FlightSearchResultsProcessedEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly IFlightSearchResultsRepository _searchResultsRepository;
        private readonly ILogger _logger;
        private readonly DaprClient _daprClient;

        public FlightSearchResultsProcessedEventHandler(
            IEventBus eventBus,
            IFlightSearchResultsRepository searchResultsRepository,
            ILogger<FlightSearchResultsProcessedEventHandler> logger,
            DaprClient daprClient)
        {
            _eventBus = eventBus;
            _searchResultsRepository = searchResultsRepository;
            _logger = logger;
            _daprClient = daprClient;
        }
        public async Task Handle(FlightSearchResultsProcessedEvent @event)
        {
            await _searchResultsRepository.UpdateAsync(new Models.FlightSearchResults()
            {
                QueryKey= @event.QueryKey,
                Itineraries = @event.Itineraries.OrderBy(i => i.Price.Amount)
                                                .ToList()
            });
        }
    }
}
