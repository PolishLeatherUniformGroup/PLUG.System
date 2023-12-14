using ONPA.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using ONPA.Common.Application;
using ONPA.IntegrationEvents;

namespace ONPA.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationReceivedDomainEventHandler : DomainEventHandlerBase<ApplicationReceivedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public ApplicationReceivedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationReceivedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent =
            new ApplicationReceivedIntegrationEvent(notification.TenantId,notification.FirstName, notification.Email,
                notification.AggregateId, notification.Recommendations.ToArray());
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}