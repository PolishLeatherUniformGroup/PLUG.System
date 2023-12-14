using ONPA.Common.Application;
using ONPA.IntegrationEvents;
using ONPA.Membership.Api.Application.IntegrationEvents;
using ONPA.Membership.DomainEvents;

namespace ONPA.Membership.Api.Application.DomainEventHandlers;

public class MemberFeePaymentRequestedDomainEventHandler 
    : DomainEventHandlerBase<MemberFeePaymentRequestedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public MemberFeePaymentRequestedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(MemberFeePaymentRequestedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new MemberRequestedFeePaymentIntegrationEvent(notification.TenantId,
            notification.FirstName, notification.Email, notification.RequestedFee.Amount,
            notification.RequestedFee.Currency, notification.DueDate, notification.Period);
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}

