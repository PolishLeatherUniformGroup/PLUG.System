using PLUG.System.Common.Application;
using PLUG.System.Membership.DomainEvents;

namespace PLUG.System.Membership.Api.Application.DomainEventHandlers;

public class MemberFeePaymentRequestedDomainEventHandler 
    : DomainEventHandlerBase<MemberFeePaymentRequestedDomainEvent>
{
    public override Task Handle(MemberFeePaymentRequestedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}