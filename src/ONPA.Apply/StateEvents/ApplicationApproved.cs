using ONPA.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationApproved : StateEventBase
{
    public DateTime ApproveDate { get; private set; }

    public ApplicationApproved(DateTime approveDate)
    {
        this.ApproveDate = approveDate;
    }

    private ApplicationApproved(Guid tenantId, Guid aggregateId, long aggregateVersion, DateTime approveDate) : base(tenantId,aggregateId, aggregateVersion)
    {
        this.ApproveDate = approveDate;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationApproved(tenantId,aggregateId, aggregateVersion, this.ApproveDate);
    }
}