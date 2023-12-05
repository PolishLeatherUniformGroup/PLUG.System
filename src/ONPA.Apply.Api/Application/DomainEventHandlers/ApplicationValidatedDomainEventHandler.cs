using ONPA.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using ONPA.Common.Application;
using ONPA.IntegrationEvents;

namespace ONPA.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationValidatedDomainEventHandler : DomainEventHandlerBase<ApplicationValidatedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public ApplicationValidatedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationValidatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ApplicationAcknowledgedIntegrationEvent(notification.FirstName, notification.Email,
            notification.RequiredFee.Amount, notification.RequiredFee.Currency);
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}