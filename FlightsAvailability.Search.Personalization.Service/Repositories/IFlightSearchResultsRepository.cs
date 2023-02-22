using FlightsAvailability.Search.Personalization.Service.Models;

namespace FlightsAvailability.Search.Personalization.Service.Repositories
{
    public interface IFlightSearchResultsRepository
    {
        Task<FlightSearchResults> GetAsync(string queryKey);
        Task<FlightSearchResults> UpdateAsync(FlightSearchResults searchResults);
        Task DeleteAsync(string queryKey);
    }
}
