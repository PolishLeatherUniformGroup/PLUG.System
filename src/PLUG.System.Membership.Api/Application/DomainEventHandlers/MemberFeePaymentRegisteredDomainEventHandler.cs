using PLUG.System.Common.Application;
using PLUG.System.IntegrationEvents;
using PLUG.System.Membership.Api.Application.IntegrationEvents;
using PLUG.System.Membership.DomainEvents;

namespace PLUG.System.Membership.Api.Application.DomainEventHandlers;

public class MemberFeePaymentRegisteredDomainEventHandler 
    : DomainEventHandlerBase<MemberFeePaymentRegisteredDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public MemberFeePaymentRegisteredDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        _integrationEventService = integrationEventService;
    }

    public override async Task Handle(MemberFeePaymentRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        if (notification.RegisteredFee >= notification.RequiredFee)
        {
            return;
        }
        var integrationEvent = new MemberFeePaymentIncompleteIntegrationEvent(
            notification.FirstName, notification.Email, notification.RegisteredFee.Amount,
            notification.RegisteredFee.Currency,notification.RequiredFee.Amount,
            notification.PaidDate,notification.Period);
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}