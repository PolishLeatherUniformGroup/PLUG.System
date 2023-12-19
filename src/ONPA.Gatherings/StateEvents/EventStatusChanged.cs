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

    private EventStatusChanged(Guid tenantId, Guid aggregateId, long aggregateVersion, EventStatus status) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.Status = status;
    }


    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new EventStatusChanged(tenantId, aggregateId, aggregateVersion, this.Status);
    }
}