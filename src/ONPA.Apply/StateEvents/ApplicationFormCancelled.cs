using ONPA.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public class ApplicationFormCancelled : StateEventBase
{
    public string Reason { get; set; }

    public ApplicationFormCancelled(string reason)
    {
        this.Reason = reason?? throw new ArgumentNullException(nameof(reason));
    }

    private ApplicationFormCancelled(Guid tenantId, Guid aggregateId, long aggregateVersion, string reason) : base(tenantId, aggregateId,
        aggregateVersion)
    {
        this.Reason = reason ?? throw new ArgumentNullException(nameof(reason));
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationFormCancelled(tenantId, aggregateId, aggregateVersion, this.Reason);
    }
}