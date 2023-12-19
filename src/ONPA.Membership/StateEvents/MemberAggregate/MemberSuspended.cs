using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class MemberSuspended : StateEventBase
{
    public MembershipSuspension Suspension { get; private set; }

    public MemberSuspended(MembershipSuspension suspension)
    {
        this.Suspension = suspension;
    }

    private MemberSuspended(Guid tenantId, Guid aggregateId, long aggregateVersion, MembershipSuspension suspension) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.Suspension = suspension;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MemberSuspended(tenantId, aggregateId, aggregateVersion, this.Suspension);
    }
}