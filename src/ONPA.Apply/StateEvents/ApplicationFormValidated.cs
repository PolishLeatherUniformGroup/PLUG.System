using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.StateEvents;

public class ApplicationFormValidated: StateEventBase
{
    public Money RequiredFee { get; set; }
    public ApplicationFormValidated(Money requiredFee)
    {
        this.RequiredFee = requiredFee;
    }

    private ApplicationFormValidated(Guid tenantId, Guid aggregateId, long aggregateVersion, Money requiredFee) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.RequiredFee = requiredFee;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationFormValidated(tenantId, aggregateId, aggregateVersion,this.RequiredFee);
    }
}