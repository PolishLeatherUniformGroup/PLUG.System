using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class ExpelAppealDismissed : StateEventBase
{
    public MembershipExpel Expel { get; private set; }
    public DateTime EffectiveDate { get; private set; }

    public ExpelAppealDismissed(MembershipExpel expel, DateTime effectiveDate)
    {
        this.Expel = expel;
        this.EffectiveDate = effectiveDate;
    }

    private ExpelAppealDismissed(Guid aggregateId, long aggregateVersion, MembershipExpel expel, DateTime effectiveDate) : base(aggregateId, aggregateVersion)
    {
        this.Expel = expel;
        this.EffectiveDate = effectiveDate;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ExpelAppealDismissed(aggregateId, aggregateVersion, this.Expel,this.EffectiveDate);
    }
}