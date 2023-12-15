using ONPA.Common.Application;
using ONPA.IntegrationEvents;
using ONPA.Membership.Api.Application.IntegrationEvents;
using ONPA.Membership.DomainEvents;

namespace ONPA.Membership.Api.Application.DomainEventHandlers;

public class MemberFeePaymentRegisteredDomainEventHandler 
    : DomainEventHandlerBase<MemberFeePaymentRegisteredDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public MemberFeePaymentRegisteredDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(MemberFeePaymentRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        if (notification.RegisteredFee >= notification.RequiredFee)
        {
            return;
        }
        var integrationEvent = new MemberFeePaymentIncompleteIntegrationEvent(notification.TenantId,
            notification.FirstName, notification.Email, notification.RegisteredFee.Amount,
            notification.RegisteredFee.Currency,notification.RequiredFee.Amount,
            notification.PaidDate,notification.Period);
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}