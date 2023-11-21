using PLUG.System.EventBus.Events;

namespace PLUG.System.Membership.Api.Application.IntegrationEvents;

public interface IIntegrationEventService
{
    Task PublishEventsThroughEventBusAsync(Guid transactionId);
    Task AddAndSaveEventAsync(IntegrationEvent evt);
}