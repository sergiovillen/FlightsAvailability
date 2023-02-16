using FlightsAvailability.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAvailability.Search.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpPost] 
        public async Task<IActionResult> Post([FromBody] FlightSearchRequest request)
        {
            var searchQuery = new FlightSearchQuery() { Key = Guid.NewGuid().ToString() };
            return Ok(searchQuery);
        }
    }
}
