using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.StateEvents;

public sealed class MemberFeeRequested : StateEventBase
{
    public MembershipFee RequestedFee { get; private set; }

    public MemberFeeRequested(MembershipFee requestedFee)
    {
        this.RequestedFee = requestedFee;
    }

    private MemberFeeRequested(Guid aggregateId, long aggregateVersion, MembershipFee requestedFee) : base(aggregateId, aggregateVersion)
    {
        this.RequestedFee = requestedFee;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberFeeRequested(aggregateId, aggregateVersion, this.RequestedFee);
    }
}