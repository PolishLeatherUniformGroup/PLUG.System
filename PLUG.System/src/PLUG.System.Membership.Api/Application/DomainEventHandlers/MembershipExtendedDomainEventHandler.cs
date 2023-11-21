using PLUG.System.Common.Application;
using PLUG.System.Membership.DomainEvents;

namespace PLUG.System.Membership.Api.Application.DomainEventHandlers;

public class MembershipExtendedDomainEventHandler 
    : DomainEventHandlerBase<MembershipExtendedDomainEvent>
{
    public override Task Handle(MembershipExtendedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}