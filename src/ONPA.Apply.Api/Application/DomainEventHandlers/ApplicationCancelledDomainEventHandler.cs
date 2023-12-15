using ONPA.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using ONPA.Common.Application;
using ONPA.IntegrationEvents;

namespace ONPA.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationCancelledDomainEventHandler : DomainEventHandlerBase<ApplicationCancelledDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public ApplicationCancelledDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationCancelledDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ApplicationCancelledIntegrationEvent(notification.TenantId,notification.FirstName, notification.Email,
            notification.Reason);
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}