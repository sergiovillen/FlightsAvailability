using Dapr.Client;
using FlightsAvailability.Search.Personalization.Service.Models;

namespace FlightsAvailability.Search.Personalization.Service.Repositories
{
    public class FlightSearchResultsRepository : IFlightSearchResultsRepository
    {
        private const string StateStoreName = "flights-availability-statestore";

        private readonly DaprClient _daprClient;
        private readonly ILogger _logger;

        public FlightSearchResultsRepository(DaprClient daprClient, ILogger<FlightSearchResultsRepository> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        public Task DeleteAsync(string queryKey) =>
            _daprClient.DeleteStateAsync(StateStoreName, queryKey);

        public Task<FlightSearchResults> GetAsync(string queryKey) =>
            _daprClient.GetStateAsync<FlightSearchResults>(StateStoreName, queryKey);

        public async Task<FlightSearchResults> UpdateAsync(FlightSearchResults searchResults)
        {
            var state = await _daprClient.GetStateEntryAsync<FlightSearchResults>(StateStoreName, searchResults.QueryKey);
            state.Value = searchResults;

            await state.SaveAsync();

            _logger.LogInformation("Search Results item persisted successfully.");

            return await GetAsync(searchResults.QueryKey);
        }
    }
}
