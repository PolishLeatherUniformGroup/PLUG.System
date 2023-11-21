using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.StateEvents;

public sealed class MemberExpelAppealReceived : StateEventBase
{
    public string Justification { get; private set; }
    public DateTime ReceivedDate { get; private set; }

    public MemberExpelAppealReceived(string justification, DateTime receivedDate)
    {
        this.Justification = justification;
        this.ReceivedDate = receivedDate;
    }

    private MemberExpelAppealReceived(Guid aggregateId, long aggregateVersion, string justification,
        DateTime receivedDate) : base(aggregateId, aggregateVersion)
    {
        this.Justification = justification;
        this.ReceivedDate = receivedDate;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberExpelAppealReceived(aggregateId, aggregateVersion, this.Justification, this.ReceivedDate);
    }
}