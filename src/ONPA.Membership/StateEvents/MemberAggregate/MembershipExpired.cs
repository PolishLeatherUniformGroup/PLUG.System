using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class MembershipExpired : StateEventBase
{
    public DateTime TerminationDate { get; private set; }
    public string TerminationReason { get; private set; }

    public MembershipExpired(DateTime terminationDate, string terminationReason)
    {
        this.TerminationDate = terminationDate;
        this.TerminationReason = terminationReason;
    }

    private MembershipExpired(Guid tenantId, Guid aggregateId, long aggregateVersion, DateTime terminationDate, string terminationReason) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.TerminationDate = terminationDate;
        this.TerminationReason = terminationReason;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MembershipExpired(tenantId,aggregateId, aggregateVersion, this.TerminationDate,this.TerminationReason);
    }
}