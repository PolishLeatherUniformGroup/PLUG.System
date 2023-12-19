using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class MemberFeeRequested : StateEventBase
{
    public MembershipFee RequestedFee { get; private set; }

    public MemberFeeRequested(MembershipFee requestedFee)
    {
        this.RequestedFee = requestedFee;
    }

    private MemberFeeRequested(Guid tenantId, Guid aggregateId, long aggregateVersion, MembershipFee requestedFee) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.RequestedFee = requestedFee;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MemberFeeRequested(tenantId, aggregateId, aggregateVersion, this.RequestedFee);
    }
}