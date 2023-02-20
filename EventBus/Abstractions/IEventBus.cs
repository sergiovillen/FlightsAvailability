using EventBus.Events;

namespace EventBus.Abstractions
{
    public interface IEventBus
    {
        Task PublishAsync(IntegrationEvent integrationEvent);
        Task PublishAsync(string topicName, IntegrationEvent integrationEvent);
    }
}
