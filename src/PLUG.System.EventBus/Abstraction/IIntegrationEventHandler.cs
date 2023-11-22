using PLUG.System.EventBus.Events;

namespace PLUG.System.EventBus.Abstraction;

public interface IIntegrationEventHandler
{
    Task Handle(IntegrationEvent @event);
}

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);

    Task IIntegrationEventHandler.Handle(IntegrationEvent @event) => this.Handle((TIntegrationEvent)@event);
}