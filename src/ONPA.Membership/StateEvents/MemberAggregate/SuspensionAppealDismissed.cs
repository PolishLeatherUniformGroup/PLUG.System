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

    public SuspensionAppealDismissed(Guid tenantId, Guid aggregateId, long aggregateVersion, MembershipSuspension suspension) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.Suspension = suspension;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new SuspensionAppealDismissed(tenantId, aggregateId, aggregateVersion, this.Suspension);
    }
}
