using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class MemberTypeChanged : StateEventBase
{
    public MembershipType MembershipType { get; private set; }

    public MemberTypeChanged(MembershipType membershipType)
    {
        this.MembershipType = membershipType;
    }

    private MemberTypeChanged(Guid tenantId, Guid aggregateId, long aggregateVersion, MembershipType membershipType) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.MembershipType = membershipType;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MemberTypeChanged(tenantId, aggregateId, aggregateVersion, this.MembershipType);
    }
}