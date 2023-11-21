using PLUG.System.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationRejectionAppealed : StateEventBase
{
    public DateTime ReceiveDate { get; private set; }
    public string Justification { get; set; }

    public ApplicationRejectionAppealed(DateTime receiveDate, string justification)
    {
        this.ReceiveDate = receiveDate;
        this.Justification = justification;
    }

    private ApplicationRejectionAppealed(Guid aggregateId, long aggregateVersion, DateTime receiveDate, string justification) : base(aggregateId, aggregateVersion)
    {
        this.ReceiveDate = receiveDate;
        this.Justification = justification;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationRejectionAppealed(aggregateId, aggregateVersion, this.ReceiveDate,this.Justification);
    }
}