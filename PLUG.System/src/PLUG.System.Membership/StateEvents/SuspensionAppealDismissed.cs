using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.StateEvents;

public sealed class SuspensionAppealDismissed : StateEventBase
{
    public MembershipSuspension Suspension { get; private set; }

    public SuspensionAppealDismissed(MembershipSuspension suspension)
    {
        this.Suspension = suspension;
    }

    public SuspensionAppealDismissed(Guid aggregateId, long aggregateVersion, MembershipSuspension suspension) : base(aggregateId, aggregateVersion)
    {
        this.Suspension = suspension;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new SuspensionAppealDismissed(aggregateId, aggregateVersion, this.Suspension);
    }
}
