using Dapr;
using FlightsAvailability.Search.ResultsProcessor.Skyscanner.IntegrationEvents.EventHandling;
using FlightsAvailability.Search.ResultsProcessor.Skyscanner.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAvailability.Search.ResultsProcessor.Skyscanner.Controllers
{
    [ApiController]
    [Route("api/skyscanner/resultsprocessor")]
    public class SkyscannerResultsProcessorController : ControllerBase
    {
        private const string DAPR_PUBSUB_NAME = "flights-availability-pubsub";

        [HttpPost("SkyscannerResultsReceived")]
        [Topic(DAPR_PUBSUB_NAME, nameof(SkyscannerResponseReceivedEvent))]
        public async Task HandleAsync(SkyscannerResponseReceivedEvent @event, [FromServices] SkyscannerResponseReceivedEventHandler handler)
        {
            await handler.Handle(@event);
        }
    }
}
