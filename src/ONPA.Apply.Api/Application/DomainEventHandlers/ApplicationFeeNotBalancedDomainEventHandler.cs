using ONPA.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using ONPA.Common.Application;
using ONPA.IntegrationEvents;

namespace ONPA.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationFeeNotBalancedDomainEventHandler : DomainEventHandlerBase<ApplicationFeeNotBalancedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public ApplicationFeeNotBalancedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationFeeNotBalancedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ApplicationFeeMissingIntegrationEvent(notification.FirstName, notification.Email,
            notification.RequiredPayment.Amount, notification.RegisteredPayment.Amount,
            notification.RequiredPayment.Currency);
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}