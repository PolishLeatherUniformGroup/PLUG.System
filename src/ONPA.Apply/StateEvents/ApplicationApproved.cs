using ONPA.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationApproved : StateEventBase
{
    public DateTime ApproveDate { get; private set; }

    public ApplicationApproved(DateTime approveDate)
    {
        this.ApproveDate = approveDate;
    }

    private ApplicationApproved(Guid aggregateId, long aggregateVersion, DateTime approveDate) : base(aggregateId, aggregateVersion)
    {
        this.ApproveDate = approveDate;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationApproved(aggregateId, aggregateVersion, this.ApproveDate);
    }
}