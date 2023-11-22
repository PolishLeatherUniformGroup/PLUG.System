using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.StateEvents;

public sealed class SuspensionAppealApproved : StateEventBase
{
    public MembershipSuspension Suspension { get; private set; }

    public SuspensionAppealApproved(MembershipSuspension suspension)
    {
        this.Suspension = suspension;
    }

    public SuspensionAppealApproved(Guid aggregateId, long aggregateVersion, MembershipSuspension suspension) : base(aggregateId, aggregateVersion)
    {
        this.Suspension = suspension;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new SuspensionAppealApproved(aggregateId, aggregateVersion, this.Suspension);
    }
}