using PLUG.System.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using PLUG.System.Common.Application;
using PLUG.System.IntegrationEvents;

namespace PLUG.System.Apply.Api.Application.DomainEventHandlers;

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