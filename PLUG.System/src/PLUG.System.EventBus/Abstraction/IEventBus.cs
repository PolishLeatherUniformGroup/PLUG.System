using PLUG.System.EventBus.Events;

namespace PLUG.System.EventBus.Abstraction;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
}