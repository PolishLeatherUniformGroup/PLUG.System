using ONPA.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationRejectionAppealReceived : StateEventBase
{
    public DateTime AppealDate { get; private set; }
    public string Justification { get; private set; }

    public ApplicationRejectionAppealReceived(DateTime appealDate, string justification)
    {
        this.AppealDate = appealDate;
        this.Justification = justification;
    }

    private ApplicationRejectionAppealReceived(Guid aggregateId, long aggregateVersion, DateTime appealDate,
        string justification) : base(aggregateId, aggregateVersion)
    {
        this.AppealDate = appealDate;
        this.Justification = justification;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationRejectionAppealReceived(aggregateId, aggregateVersion, this.AppealDate, this.Justification);
    }
}