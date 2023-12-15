using ONPA.Common.Domain;

namespace ONPA.Gatherings.StateEvents;

public sealed class EventCapacityChanged :StateEventBase
{
    public int? Capacity { get; private set; }

    public EventCapacityChanged(int? capacity)
    {
        this.Capacity = capacity;
    }

    private EventCapacityChanged(Guid aggregateId, long aggregateVersion, int? capacity) : base(aggregateId, aggregateVersion)
    {
        this.Capacity = capacity;
    }


    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new EventCapacityChanged(aggregateId, aggregateVersion, this.Capacity);
    }
}