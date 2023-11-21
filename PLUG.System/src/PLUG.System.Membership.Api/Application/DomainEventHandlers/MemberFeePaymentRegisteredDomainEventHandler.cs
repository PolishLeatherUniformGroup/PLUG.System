using PLUG.System.Common.Application;
using PLUG.System.Membership.DomainEvents;

namespace PLUG.System.Membership.Api.Application.DomainEventHandlers;

public class MemberFeePaymentRegisteredDomainEventHandler 
    : DomainEventHandlerBase<MemberFeePaymentRegisteredDomainEvent>
{
    public override Task Handle(MemberFeePaymentRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}