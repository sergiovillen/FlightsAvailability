﻿using EventBus.Events;

namespace FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.Events
{
    public record FlightSearchQueryReceivedEvent : IntegrationEvent
    {
        public string QueryKey { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime? Time { get; set; }
    }
}
