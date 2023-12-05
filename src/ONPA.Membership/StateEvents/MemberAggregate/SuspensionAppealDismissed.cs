using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

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
