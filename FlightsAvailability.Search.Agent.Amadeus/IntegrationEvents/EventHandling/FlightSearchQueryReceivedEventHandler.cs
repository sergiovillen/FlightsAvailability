﻿using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.Events;
using Google.Type;
using Grpc.Core;
using System.Text;
using System;
using System.Text.Json;
using FlightsAvailability.Search.Agent.Amadeus.Entities;
using static Google.Rpc.Context.AttributeContext.Types;

namespace FlightsAvailability.Search.Agent.Amadeus.IntegrationEvents.EventHandling
{
    public class FlightSearchQueryReceivedEventHandler : IIntegrationEventHandler<FlightSearchQueryReceivedEvent>
    {
        private const string DAPR_BINDING_AMADEUS_GET_TOKEN = "amadeus-token";
        private const string DAPR_BINDING_AMADEUS_FLIGHT_OFFERS = "amadeus-flight-offers";
        private const string SECRET_STORE_NAME = "flights-availability-secretstore";
        private const string AMADEUS_API_CLIENT_CREDENTIALS_SECRET_NAME = "amaudeus-api-client-credentials";
        private const string AMADEUS_API_CONTENT_TYPE = "application/x-www-form-urlencoded";
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
            string data = $"grant_type=client_credentials&client_id={clientCredentials["client_id"]}&client_secret={clientCredentials["client_secret"]}";
            var req = new BindingRequest(DAPR_BINDING_AMADEUS_GET_TOKEN, "post");
            req.Metadata.Add("Content-Type", AMADEUS_API_CONTENT_TYPE);
            req.Data = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(data));
            var bindingResponse = await _daprClient.InvokeBindingAsync(req);
            var amadeusToken = JsonSerializer.Deserialize<AmadeusTokenResponse>(Encoding.UTF8.GetString(bindingResponse.Data.Span))!;

            //Calling Flight Offers
            var metadata = new Dictionary<string, string>();
            metadata.Add("Authorization", $"Bearer {amadeusToken.access_token}");
            metadata.Add("path", "/flight-offers?originLocationCode=MAD&destinationLocationCode=CDG&departureDate=2023-05-02&adults=1&nonStop=false&max=250");
            var searchPollResponse = await _daprClient.InvokeBindingAsync<object, dynamic>(DAPR_BINDING_AMADEUS_FLIGHT_OFFERS, "get", string.Empty, (IReadOnlyDictionary<string, string>)metadata);

        }
    }
}