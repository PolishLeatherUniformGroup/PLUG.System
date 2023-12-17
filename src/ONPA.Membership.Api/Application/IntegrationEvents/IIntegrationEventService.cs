using System.Diagnostics.CodeAnalysis;
using ONPA.EventBus.Events;

namespace ONPA.Membership.Api.Application.IntegrationEvents;

public interface IIntegrationEventService
{
    Task PublishEventsThroughEventBusAsync(Guid transactionId);
    Task AddAndSaveEventAsync(IntegrationEvent evt);
}