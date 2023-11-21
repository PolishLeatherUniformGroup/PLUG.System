using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.StateEvents;

public sealed class MemberSuspensionAppealReceived : StateEventBase
{
    public string Justification { get; private set; }
    public DateTime ReceivedDate { get; private set; }

    public MemberSuspensionAppealReceived(string justification, DateTime receivedDate)
    {
        this.Justification = justification;
        this.ReceivedDate = receivedDate;
    }

    private MemberSuspensionAppealReceived(Guid aggregateId, long aggregateVersion, string justification,
        DateTime receivedDate) : base(aggregateId, aggregateVersion)
    {
        this.Justification = justification;
        this.ReceivedDate = receivedDate;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberSuspensionAppealReceived(aggregateId, aggregateVersion, Justification, ReceivedDate);
    }
}