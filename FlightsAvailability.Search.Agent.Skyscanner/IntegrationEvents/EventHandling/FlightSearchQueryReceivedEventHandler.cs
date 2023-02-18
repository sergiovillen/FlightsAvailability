using Dapr.Client;
using EventBus.Abstractions;
using FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.Events;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;

namespace FlightsAvailability.Search.Agent.Skyscanner.IntegrationEvents.EventHandling
{
    public class FlightSearchQueryReceivedEventHandler : IIntegrationEventHandler<FlightSearchQueryReceivedEvent>
    {
        private const string DAPR_BINDING_NAME = "flights-availability-binding-skyscanner";
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
            var metadata = new Dictionary<string, string>();
            metadata.Add("X-api-key", "prtl6749387986743898559646983194");            
            var res = await _daprClient.InvokeBindingAsync<string,dynamic>(DAPR_BINDING_NAME, "get",string.Empty, (IReadOnlyDictionary<string, string>) metadata);

            var request = new BindingRequest(DAPR_BINDING_NAME, "get");
            request.Metadata.Add("X-api-key", "prtl6749387986743898559646983194");
            var response = await _daprClient.InvokeBindingAsync(request);

            await Task.Delay(3000);
        }
    }
}
