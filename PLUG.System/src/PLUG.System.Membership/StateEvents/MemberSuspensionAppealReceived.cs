using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.StateEvents;

public sealed class MemberSuspensionAppealReceived : StateEventBase
{
    public MembershipSuspension Suspension { get; private set; }

    public MemberSuspensionAppealReceived(MembershipSuspension suspension)
    {
        this.Suspension = suspension;
    }

    public MemberSuspensionAppealReceived(Guid aggregateId, long aggregateVersion, MembershipSuspension suspension) : base(aggregateId, aggregateVersion)
    {
        this.Suspension = suspension;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberSuspensionAppealReceived(aggregateId, aggregateVersion, this.Suspension);
    }
}