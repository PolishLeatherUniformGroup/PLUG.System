using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public sealed class MemberLeft : StateEventBase
{
    public DateTime TerminationDate { get; private set; }
    public string TerminationReason { get; private set; }

    public MemberLeft(DateTime terminationDate, string terminationReason)
    {
        this.TerminationDate = terminationDate;
        this.TerminationReason = terminationReason;
    }

    private MemberLeft(Guid tenantId, Guid aggregateId, long aggregateVersion, DateTime terminationDate, string terminationReason) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.TerminationDate = terminationDate;
        this.TerminationReason = terminationReason;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MemberLeft(tenantId,aggregateId, aggregateVersion, this.TerminationDate,this.TerminationReason);
    }
}