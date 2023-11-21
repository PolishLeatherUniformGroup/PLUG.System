using PLUG.System.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationRejectionAppealApproved : StateEventBase
{
    public DateTime ApproveDate { get; private set; }

    public ApplicationRejectionAppealApproved(DateTime approveDate)
    {
        this.ApproveDate = approveDate;
    }

    private ApplicationRejectionAppealApproved(Guid aggregateId, long aggregateVersion, DateTime approveDate) : base(aggregateId, aggregateVersion)
    {
        this.ApproveDate = approveDate;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationRejectionAppealApproved(aggregateId, aggregateVersion, this.ApproveDate);
    }
}