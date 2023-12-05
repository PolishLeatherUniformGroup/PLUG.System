using ONPA.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using ONPA.Common.Application;
using ONPA.IntegrationEvents;

namespace ONPA.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationRejectedDomainEventHandler : DomainEventHandlerBase<ApplicationRejectedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public ApplicationRejectedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationRejectedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ApplicationRejectedIntegrationEvent(notification.FirstName, notification.Email,
            notification.DecisionDetail, notification.AppealDeadline);
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}