﻿using EventBus.Abstractions;
using EventBus;
using Healthchecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.EventHandling;

namespace FlightsAvailability.Search.Agent.Amadeus
{
    public static class ProgramExtensions
    {
        public static void AddCustomMvc(this WebApplicationBuilder builder)
        {
            // TODO DaprClient good enough?
            builder.Services.AddControllers().AddDapr();
        }

        public static void AddCustomHealthChecks(this WebApplicationBuilder builder) =>
            builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDapr();

        public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEventBus, DaprEventBus>();
            builder.Services.AddScoped<FlightSearchQueryReceivedEventHandler>();
        }
    }
}
