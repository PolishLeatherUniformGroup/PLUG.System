using ONPA.EventBus.Events;

namespace ONPA.EventBus.Abstraction;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
}