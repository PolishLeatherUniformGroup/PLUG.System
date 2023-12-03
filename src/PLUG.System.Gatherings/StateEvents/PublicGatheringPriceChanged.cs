using PLUG.System.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Gatherings.StateEvents;

public sealed class PublicGatheringPriceChanged :StateEventBase
{
    public Money Price { get; private set; }

    public PublicGatheringPriceChanged(Money price)
    {
        this.Price = price;
    }

    private PublicGatheringPriceChanged(Guid aggregateId, long aggregateVersion, Money price) : base(aggregateId, aggregateVersion)
    {
        this.Price = price;
    }


    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new PublicGatheringPriceChanged(aggregateId, aggregateVersion, this.Price);
    }
}