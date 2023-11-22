using PLUG.System.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using PLUG.System.Common.Application;
using PLUG.System.IntegrationEvents;

namespace PLUG.System.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationFeeBalancedDomainEventHandler : DomainEventHandlerBase<ApplicationFeeBalancedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public ApplicationFeeBalancedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationFeeBalancedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ApplicationDecisionsExpectedIntegrationEvent(notification.FirstName, notification.Email,
            notification.ExpectedDecisionDate);
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}