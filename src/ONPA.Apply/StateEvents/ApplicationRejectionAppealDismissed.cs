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

    private ApplicationRejectionAppealDismissed(Guid aggregateId, long aggregateVersion, DateTime rejectDate, string decisionDetails) : base(aggregateId, aggregateVersion)
    {
        this.RejectDate = rejectDate;
        this.DecisionDetails = decisionDetails;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationRejectionAppealDismissed(aggregateId, aggregateVersion, this.RejectDate,this.DecisionDetails);
    }
}