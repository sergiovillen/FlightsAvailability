﻿using Dapr;
using FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.EventHandling;
using FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;

namespace FlightsAvailability.Search.Agent.Skyscanner.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class FlightSearchEventController : ControllerBase
    {
        private const string DAPR_PUBSUB_NAME = "flights-availability-pubsub";

        [HttpPost("FlightSearchQueryReceived")]
        [Topic(DAPR_PUBSUB_NAME, nameof(FlightSearchQueryReceivedEvent))]
        public async Task HandleAsync(FlightSearchQueryReceivedEvent @event, [FromServices] FlightSearchQueryReceivedEventHandler handler)
        {
            await handler.Handle(@event);
        }
    }
}
