using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class SuspensionAppealApproved : StateEventBase
{
    public MembershipSuspension Suspension { get; private set; }

    public SuspensionAppealApproved(MembershipSuspension suspension)
    {
        this.Suspension = suspension;
    }

    public SuspensionAppealApproved(Guid tenantId, Guid aggregateId, long aggregateVersion, MembershipSuspension suspension) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.Suspension = suspension;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new SuspensionAppealApproved(tenantId, aggregateId, aggregateVersion, this.Suspension);
    }
}