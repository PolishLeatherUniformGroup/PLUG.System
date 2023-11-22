using PLUG.System.Common.Application;
using PLUG.System.IntegrationEvents;
using PLUG.System.Membership.Api.Application.IntegrationEvents;
using PLUG.System.Membership.DomainEvents;

namespace PLUG.System.Membership.Api.Application.DomainEventHandlers;

public class MemberFeePaymentRequestedDomainEventHandler 
    : DomainEventHandlerBase<MemberFeePaymentRequestedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public MemberFeePaymentRequestedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        _integrationEventService = integrationEventService;
    }

    public override async Task Handle(MemberFeePaymentRequestedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new MemberRequestedFeePaymentIntegrationEvent(
            notification.FirstName, notification.Email, notification.RequestedFee.Amount,
            notification.RequestedFee.Currency, notification.DueDate, notification.Period);
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}

