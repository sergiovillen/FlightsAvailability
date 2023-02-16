using EventBus.Abstractions;
using FlightsAvailability.Search.Service.Dto;
using FlightsAvailability.Search.Service.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAvailability.Search.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IEventBus _eventBus;

        public SearchController(IEventBus eventBus, ILogger<SearchController> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        [HttpPost] 
        public async Task<IActionResult> Post([FromBody] FlightSearchRequest request)
        {
            var searchQuery = new FlightSearchQuery() { Key = Guid.NewGuid().ToString() };
            var eventMessage = new FlightSearchQueryReceivedEvent() { 
                From= request.From,
                QueryKey = searchQuery.Key,
                Time = request.Time,
                To = request.To
            };
            await _eventBus.PublishAsync(eventMessage);
            return Ok(searchQuery);
        }
    }
}
