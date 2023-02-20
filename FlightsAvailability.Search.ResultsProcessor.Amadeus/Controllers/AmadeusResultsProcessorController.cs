using Dapr;
using FlightsAvailability.Search.ResultsProcessor.Amadeus.IntegrationEvents.EventHandling;
using FlightsAvailability.Search.ResultsProcessor.Amadeus.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAvailability.Search.ResultsProcessor.Amadeus.Controllers
{
    [ApiController]
    [Route("api/amadeus/resultsprocessor")]
    public class AmadeusResultsProcessorController : ControllerBase
    {
        private const string DAPR_PUBSUB_NAME = "flights-availability-pubsub";

        [HttpPost("AmadeusResultsReceived")]
        [Topic(DAPR_PUBSUB_NAME, nameof(AmadeusResponseReceivedEvent))]
        public async Task HandleAsync(AmadeusResponseReceivedEvent @event, [FromServices] AmadeusResponseReceivedEventHandler handler)
        {
            await handler.Handle(@event);
        }
    }
}
