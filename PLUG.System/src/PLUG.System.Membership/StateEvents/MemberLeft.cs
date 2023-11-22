using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.StateEvents;

public sealed class MemberLeft : StateEventBase
{
    public DateTime TerminationDate { get; set; }

    public MemberLeft(DateTime terminationDate)
    {
        this.TerminationDate = terminationDate;
    }

    private MemberLeft(Guid aggregateId, long aggregateVersion, DateTime terminationDate) : base(aggregateId, aggregateVersion)
    {
        this.TerminationDate = terminationDate;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberLeft(aggregateId, aggregateVersion, this.TerminationDate);
    }
}