using ONPA.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using ONPA.Common.Application;
using ONPA.IntegrationEvents;

namespace ONPA.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationFeeBalancedDomainEventHandler : DomainEventHandlerBase<ApplicationFeeBalancedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public ApplicationFeeBalancedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationFeeBalancedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ApplicationDecisionsExpectedIntegrationEvent(notification.TenantId,notification.FirstName, notification.Email,
            notification.ExpectedDecisionDate);
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}