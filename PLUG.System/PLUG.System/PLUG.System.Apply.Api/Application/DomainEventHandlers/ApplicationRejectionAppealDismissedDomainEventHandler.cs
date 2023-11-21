using PLUG.System.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using PLUG.System.Common.Application;
using PLUG.System.IntegrationEvents;

namespace PLUG.System.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationRejectionAppealDismissedDomainEventHandler : DomainEventHandlerBase<ApplicationRejectionAppealDismissedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public ApplicationRejectionAppealDismissedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationRejectionAppealDismissedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ApplicationRejectionAppealDismissedIntegrationEvent(notification.FirstName, notification.Email,
            notification.RejectDate,notification.DecisionDetails);
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
    
    
}