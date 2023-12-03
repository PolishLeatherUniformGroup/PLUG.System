using PLUG.System.Common.Domain;

namespace PLUG.System.Gatherings.StateEvents;

public sealed class PublicGatheringCapacityChanged :StateEventBase
{
    public int? Capacity { get; private set; }

    public PublicGatheringCapacityChanged(int? capacity)
    {
        this.Capacity = capacity;
    }

    private PublicGatheringCapacityChanged(Guid aggregateId, long aggregateVersion, int? capacity) : base(aggregateId, aggregateVersion)
    {
        this.Capacity = capacity;
    }


    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new PublicGatheringCapacityChanged(aggregateId, aggregateVersion, this.Capacity);
    }
}