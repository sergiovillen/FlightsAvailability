using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.Events;
using Google.Type;
using Grpc.Core;
using System.Text;
using System;
using System.Text.Json;

namespace FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.EventHandling
{
    public class FlightSearchQueryReceivedEventHandler : IIntegrationEventHandler<FlightSearchQueryReceivedEvent>
    {
        private const string SECRET_STORE_NAME = "flights-availability-secretstore";
        private const string AMADEUS_API_CLIENT_CREDENTIALS_SECRET_NAME = "amaudeus-api-client-credentials";
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;
        private readonly DaprClient _daprClient;

        public FlightSearchQueryReceivedEventHandler(
            IEventBus eventBus,
            ILogger<FlightSearchQueryReceivedEventHandler> logger,
            DaprClient daprClient)
        {
            _eventBus = eventBus;
            _logger = logger;
            _daprClient = daprClient;
        }
        public async Task Handle(FlightSearchQueryReceivedEvent @event)
        {
            var clientCredentials = await _daprClient.GetSecretAsync(SECRET_STORE_NAME, AMADEUS_API_CLIENT_CREDENTIALS_SECRET_NAME);
            var metadata = new Dictionary<string, string>();
            metadata.Add("Content-Type", "application/x-www-form-urlencoded");
            string data = $"grant_type=client_credentials&client_id={clientCredentials["client_id"]}&client_secret={clientCredentials["client_secret"]}";
            var req = new BindingRequest("amadeus-token", "post");
            req.Metadata.Add("Content-Type", "application/x-www-form-urlencoded");
            req.Data = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(data));
            var response = await _daprClient.InvokeBindingAsync(req);
            var json = Encoding.UTF8.GetString(response.Data.Span);
        }
    }
}
