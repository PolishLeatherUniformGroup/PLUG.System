using ONPA.Common.Domain;
using ONPA.Organizations.Domain;

namespace ONPA.Organizations.StateEvents;

public class MembershipFeeRequested : StateEventBase
{
    public MembershipFee Fee { get; private set; }

    public MembershipFeeRequested(MembershipFee fee)
    {
        this.Fee = fee;
    }
    
    private MembershipFeeRequested(Guid aggregateId, long aggregateVersion, MembershipFee fee) : base(aggregateId, aggregateVersion)
    {
        this.Fee = fee;
    }
    
    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MembershipFeeRequested(aggregateId, aggregateVersion, this.Fee);
    }
}