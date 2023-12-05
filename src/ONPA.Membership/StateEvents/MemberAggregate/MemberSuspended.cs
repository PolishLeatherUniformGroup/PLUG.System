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

    private MemberSuspended(Guid aggregateId, long aggregateVersion, MembershipSuspension suspension) : base(aggregateId, aggregateVersion)
    {
        this.Suspension = suspension;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberSuspended(aggregateId, aggregateVersion, this.Suspension);
    }
}