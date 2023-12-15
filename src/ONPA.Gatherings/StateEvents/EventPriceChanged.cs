using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.StateEvents;

public sealed class EventPriceChanged :StateEventBase
{
    public Money Price { get; private set; }

    public EventPriceChanged(Money price)
    {
        this.Price = price;
    }

    private EventPriceChanged(Guid aggregateId, long aggregateVersion, Money price) : base(aggregateId, aggregateVersion)
    {
        this.Price = price;
    }


    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new EventPriceChanged(aggregateId, aggregateVersion, this.Price);
    }
}