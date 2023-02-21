using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.Events;
using System.Text;
using System.Text.Json;
using EventBus.Events;
using FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.BindingResponses;
using static Google.Rpc.Context.AttributeContext.Types;
using static Grpc.Core.Metadata;
using Secrets.Abstractions;

namespace FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.EventHandling
{
    public class FlightSearchQueryReceivedEventHandler : IIntegrationEventHandler<FlightSearchQueryReceivedEvent>
    {
        private const string DAPR_BINDING_AMADEUS_GET_TOKEN = "amadeus-token";
        private const string DAPR_BINDING_AMADEUS_FLIGHT_OFFERS = "amadeus-flight-offers";
        private const string AMADEUS_API_CLIENT_CREDENTIALS_SECRET_NAME = "amaudeus-api-client-credentials";
        private const string AMADEUS_API_CONTENT_TYPE = "application/x-www-form-urlencoded";
        private const int AMADEUS_PARAM_ADULTS = 1;
        private readonly IEventBus _eventBus;
        private readonly ISecretsStore _secretsStore;
        private readonly ILogger _logger;
        private readonly DaprClient _daprClient;

        public FlightSearchQueryReceivedEventHandler(
            IEventBus eventBus,
            ISecretsStore secretsStore,
            ILogger<FlightSearchQueryReceivedEventHandler> logger,
            DaprClient daprClient)
        {
            _eventBus = eventBus;
            _secretsStore = secretsStore;
            _logger = logger;
            _daprClient = daprClient;
        }
        public async Task Handle(FlightSearchQueryReceivedEvent @event)
        {
            var clientCredentials = await _secretsStore.GetSecretAsync(AMADEUS_API_CLIENT_CREDENTIALS_SECRET_NAME);
            string data = $"grant_type=client_credentials&client_id={clientCredentials["client_id"]}&client_secret={clientCredentials["client_secret"]}";
            var req = new BindingRequest(DAPR_BINDING_AMADEUS_GET_TOKEN, "post");
            req.Metadata.Add("Content-Type", AMADEUS_API_CONTENT_TYPE);
            req.Data = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(data));
            var bindingResponse = await _daprClient.InvokeBindingAsync(req);
            var amadeusToken = JsonSerializer.Deserialize<AmadeusTokenResponse>(Encoding.UTF8.GetString(bindingResponse.Data.Span))!;

            //Calling Flight Offers
            var metadata = new Dictionary<string, string>()
            {
                {"Authorization", $"Bearer {amadeusToken.access_token}"},
                {"path", $"/flight-offers?originLocationCode={@event.From}&destinationLocationCode={@event.To}&departureDate={@event.Time.Value.Year}-{@event.Time.Value.Month.ToString("D2")}-{@event.Time.Value.Day.ToString("D2")}&adults={AMADEUS_PARAM_ADULTS}&nonStop=false&max=250"}
            };
            var searchPollResponse = await _daprClient.InvokeBindingAsync<object, dynamic>(DAPR_BINDING_AMADEUS_FLIGHT_OFFERS, "get", string.Empty, (IReadOnlyDictionary<string, string>)metadata);

            await _eventBus.PublishAsync(new AmadeusResponseReceivedEvent()
            {
                ParentEventId = @event.Id,
                QueryKey = @event.QueryKey,
                RawData = searchPollResponse.ToString()
            });
        }
    }
}
