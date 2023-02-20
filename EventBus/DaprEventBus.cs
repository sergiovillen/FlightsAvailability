﻿using Dapr.Client;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class DaprEventBus : IEventBus
    {
        private const string PubSubName = "flights-availability-pubsub";

        private readonly DaprClient _dapr;
        private readonly ILogger _logger;

        public DaprEventBus(DaprClient dapr, ILogger<DaprEventBus> logger)
        {
            _dapr = dapr;
            _logger = logger;
        }

        public async Task PublishAsync(IntegrationEvent integrationEvent)
        {
            var topicName = integrationEvent.GetType().Name;
            await PublishAsync(topicName, integrationEvent);
        }

        public async Task PublishAsync(string topicName, IntegrationEvent integrationEvent)
        {
            _logger.LogInformation(
                "Publishing event {@Event} to {PubsubName}.{TopicName}",
                integrationEvent,
                PubSubName,
                topicName);

            // We need to make sure that we pass the concrete type to PublishEventAsync,
            // which can be accomplished by casting the event to dynamic. This ensures
            // that all event fields are properly serialized.
            await _dapr.PublishEventAsync(PubSubName, topicName, (object)integrationEvent);
        }
    }
}
