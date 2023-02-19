using EventBus.Abstractions;
using EventBus;
using Healthchecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.EventHandling;
using Dapr.Client;
using Dapr.Extensions.Configuration;

namespace FlightsAvailability.Search.Agent.Amadeus
{
    public static class ProgramExtensions
    {
        private const string SECRET_STORE_NAME = "flights-availability-secretstore";

        public static void AddCustomConfiguration(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddDaprSecretStore(
               SECRET_STORE_NAME,
               new DaprClientBuilder().Build(), TimeSpan.FromSeconds(10));
        }

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
