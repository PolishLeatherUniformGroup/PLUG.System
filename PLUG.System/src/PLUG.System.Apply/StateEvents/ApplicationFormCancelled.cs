using PLUG.System.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public class ApplicationFormCancelled : StateEventBase
{
    public string Reason { get; set; }

    public ApplicationFormCancelled(string reason)
    {
        this.Reason = reason?? throw new ArgumentNullException(nameof(reason));
    }

    private ApplicationFormCancelled(Guid aggregateId, long aggregateVersion, string reason) : base(aggregateId,
        aggregateVersion)
    {
        this.Reason = reason ?? throw new ArgumentNullException(nameof(reason));
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationFormCancelled(aggregateId, aggregateVersion, this.Reason);
    }
}