using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.StateEvents;

public sealed class SuspensionAppealDismissed : StateEventBase
{
    public DateTime RejectDate { get; private set; }
    public string Justification { get; set; }

    public SuspensionAppealDismissed(DateTime rejectDate, string justification)
    {
        this.RejectDate = rejectDate;
        this.Justification = justification;
    }

    private SuspensionAppealDismissed(Guid aggregateId, long aggregateVersion, DateTime rejectDate, string justification) : base(aggregateId, aggregateVersion)
    {
        this.RejectDate = rejectDate;
        this.Justification = justification;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new SuspensionAppealDismissed(aggregateId, aggregateVersion, this.RejectDate,this.Justification);
    }
}