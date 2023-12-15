using ONPA.Gatherings.Domain;
using ONPA.Common.Domain;

namespace ONPA.Gatherings.StateEvents;

public sealed class EventStatusChanged :StateEventBase
{
    public EventStatus Status { get; private set; }

    public EventStatusChanged(EventStatus status)
    {
        this.Status = status;
    }

    private EventStatusChanged(Guid aggregateId, long aggregateVersion, EventStatus status) : base(aggregateId, aggregateVersion)
    {
        this.Status = status;
    }


    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new EventStatusChanged(aggregateId, aggregateVersion, this.Status);
    }
}