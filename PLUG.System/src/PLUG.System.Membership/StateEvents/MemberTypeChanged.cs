using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.StateEvents;

public sealed class MemberTypeChanged : StateEventBase
{
    public MembershipType MembershipType { get; private set; }

    public MemberTypeChanged(MembershipType membershipType)
    {
        MembershipType = membershipType;
    }

    private MemberTypeChanged(Guid aggregateId, long aggregateVersion, MembershipType membershipType) : base(aggregateId, aggregateVersion)
    {
        MembershipType = membershipType;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberTypeChanged(aggregateId, aggregateVersion, this.MembershipType);
    }
}