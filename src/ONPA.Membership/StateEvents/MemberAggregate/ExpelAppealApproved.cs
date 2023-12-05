using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class ExpelAppealApproved : StateEventBase
{
    public MembershipExpel Expel { get; set; }

    public ExpelAppealApproved(MembershipExpel expel)
    {
        this.Expel = expel;
    }

    private ExpelAppealApproved(Guid aggregateId, long aggregateVersion, MembershipExpel expel) : base(aggregateId, aggregateVersion)
    {
        this.Expel = expel;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ExpelAppealApproved(aggregateId, aggregateVersion, this.Expel);
    }
}