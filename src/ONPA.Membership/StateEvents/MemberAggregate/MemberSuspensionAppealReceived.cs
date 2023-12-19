using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class MemberSuspensionAppealReceived : StateEventBase
{
    public MembershipSuspension Suspension { get; private set; }

    public MemberSuspensionAppealReceived(MembershipSuspension suspension)
    {
        this.Suspension = suspension;
    }

    public MemberSuspensionAppealReceived(Guid tenantId, Guid aggregateId, long aggregateVersion, MembershipSuspension suspension) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.Suspension = suspension;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MemberSuspensionAppealReceived(tenantId, aggregateId, aggregateVersion, this.Suspension);
    }
}