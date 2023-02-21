using Dapr;
using FlightsAvailability.Entities.Events;
using FlightsAvailability.Search.Personalization.Service.IntegrationEvents.EventHandling;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAvailability.Search.Personalization.Service.Controllers
{
    [ApiController]
    [Route("api/search/personalization")]
    public class SearchPersonalizationController : ControllerBase
    {
        private const string DAPR_PUBSUB_NAME = "flights-availability-pubsub";

        [HttpPost("SearchPersonalization")]
        [Topic(DAPR_PUBSUB_NAME, nameof(FlightSearchResultsProcessedEvent))]
        public async Task HandleAsync(FlightSearchResultsProcessedEvent @event, [FromServices] FlightSearchResultsProcessedEventHandler handler)
        {
            await handler.Handle(@event);
        }
    }
}
