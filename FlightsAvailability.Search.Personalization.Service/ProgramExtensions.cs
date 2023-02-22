using EventBus;
using EventBus.Abstractions;
using FlightsAvailability.Search.Personalization.Service.IntegrationEvents.EventHandling;
using FlightsAvailability.Search.Personalization.Service.Repositories;
using Healthchecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Secrets;
using Secrets.Abstractions;

namespace FlightsAvailability.Search.Personalization.Service
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
            builder.Services.AddScoped<ISecretsStore, DaprSecretsStore>();
            builder.Services.AddScoped<FlightSearchResultsProcessedEventHandler>();
            builder.Services.AddScoped<IFlightSearchResultsRepository, FlightSearchResultsRepository>();
        }
    }
}
