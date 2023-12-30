using ONPA.EventBus.Events;

namespace ONPA.EventBus.Abstraction;

public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }
    event EventHandler<string> OnEventRemoved;

    void AddSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    void RemoveSubscription<T, TH>()
        where TH : IIntegrationEventHandler<T>
        where T : IntegrationEvent;

    bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
    bool HasSubscriptionsForEvent(string eventName);
    Type GetEventTypeByName(string eventName);
    void Clear();
    IEnumerable<EventBusSubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
    IEnumerable<EventBusSubscriptionInfo> GetHandlersForEvent(string eventName);
    string GetEventKey<T>();
}