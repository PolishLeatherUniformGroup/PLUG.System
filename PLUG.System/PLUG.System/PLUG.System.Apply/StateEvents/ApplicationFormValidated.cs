using PLUG.System.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.StateEvents;

public class ApplicationFormValidated: StateEventBase
{
    public Money RequiredFee { get; set; }
    public ApplicationFormValidated(Money requiredFee)
    {
        this.RequiredFee = requiredFee;
    }

    private ApplicationFormValidated(Guid aggregateId, long aggregateVersion, Money requiredFee) : base(aggregateId, aggregateVersion)
    {
        this.RequiredFee = requiredFee;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationFormValidated(aggregateId, aggregateVersion,this.RequiredFee);
    }
}