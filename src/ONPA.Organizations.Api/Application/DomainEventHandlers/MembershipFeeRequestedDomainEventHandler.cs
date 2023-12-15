using ONPA.Common.Application;
using ONPA.IntegrationEvents;
using ONPA.Organizations.Api.Application.IntegrationEvents;
using ONPA.Organizations.DomainEvents;
using PLUG.System.SharedDomain.Helpers;

namespace ONPA.Organizations.Api.Application.DomainEventHandlers;

public sealed class MembershipFeeRequestedDomainEventHandler : DomainEventHandlerBase<MembershipFeeRequestedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;
    
    public override async Task Handle(MembershipFeeRequestedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new AllMembershipFeeRequestedIntegrationEvent(notification.TenantId, notification.Fee.YearlyAmount.Amount,
            notification.Fee.YearlyAmount.Currency, notification.PaymentDeadline, notification.PaymentDeadline.ToYearEnd());
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}