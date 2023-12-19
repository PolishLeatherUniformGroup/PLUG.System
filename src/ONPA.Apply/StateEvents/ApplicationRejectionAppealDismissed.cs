using ONPA.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationRejectionAppealDismissed : StateEventBase
{
    public DateTime RejectDate { get; private set; }
    public string DecisionDetails { get; set; }

    public ApplicationRejectionAppealDismissed(DateTime rejectDate, string decisionDetails)
    {
        this.RejectDate = rejectDate;
        this.DecisionDetails = decisionDetails;
    }

    private ApplicationRejectionAppealDismissed(Guid tenantId, Guid aggregateId, long aggregateVersion, DateTime rejectDate, string decisionDetails) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.RejectDate = rejectDate;
        this.DecisionDetails = decisionDetails;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationRejectionAppealDismissed(tenantId, aggregateId, aggregateVersion, this.RejectDate,this.DecisionDetails);
    }
}