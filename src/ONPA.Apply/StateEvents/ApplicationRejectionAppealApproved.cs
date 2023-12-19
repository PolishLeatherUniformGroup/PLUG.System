using ONPA.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationRejectionAppealApproved : StateEventBase
{
    public DateTime ApproveDate { get; private set; }

    public ApplicationRejectionAppealApproved(DateTime approveDate)
    {
        this.ApproveDate = approveDate;
    }

    private ApplicationRejectionAppealApproved(Guid tenantId, Guid aggregateId, long aggregateVersion, DateTime approveDate) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.ApproveDate = approveDate;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationRejectionAppealApproved(tenantId, aggregateId, aggregateVersion, this.ApproveDate);
    }
}