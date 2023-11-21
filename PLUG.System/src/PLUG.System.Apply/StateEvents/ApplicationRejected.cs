using PLUG.System.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationRejected : StateEventBase
{
    public DateTime RejectDate { get; private set; }
    public string DecisionDetails { get; private set; }
    public DateTime AppealDeadline { get; private set; }

    public ApplicationRejected(DateTime rejectDate, string decisionDetails, DateTime appealDeadline)
    {
        this.RejectDate = rejectDate;
        this.DecisionDetails = decisionDetails;
        this.AppealDeadline = appealDeadline;
    }

    public ApplicationRejected(Guid aggregateId, long aggregateVersion, DateTime rejectDate, string decisionDetails, DateTime appealDeadline) : base(aggregateId, aggregateVersion)
    {
        this.RejectDate = rejectDate;
        this.DecisionDetails = decisionDetails;
        this.AppealDeadline = appealDeadline;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationRejected(aggregateId, aggregateVersion, this.RejectDate,this.DecisionDetails,this.AppealDeadline);
    }
}