using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.StateEvents;

public sealed class SuspensionAppealApproved : StateEventBase
{
    public DateTime ApproveDate { get; private set; }
    public string Justification { get; set; }

    public SuspensionAppealApproved(DateTime approveDate, string justification)
    {
        this.ApproveDate = approveDate;
        this.Justification = justification;
    }

    private SuspensionAppealApproved(Guid aggregateId, long aggregateVersion, DateTime approveDate, string justification) : base(aggregateId, aggregateVersion)
    {
        this.ApproveDate = approveDate;
        this.Justification = justification;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new SuspensionAppealApproved(aggregateId, aggregateVersion, this.ApproveDate,this.Justification);
    }
}