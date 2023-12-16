using ONPA.Common.Domain;

namespace ONPA.Gatherings.DomainEvents;

public class EventCapacityIncreasedDomainEvent : DomainEventBase
{
    public int? NewCapacity { get; private set; }

    public EventCapacityIncreasedDomainEvent(int? newCapacity)
    {
        this.NewCapacity = newCapacity;
    }
        
    private EventCapacityIncreasedDomainEvent(Guid aggregateId, Guid tenantId, int? newCapacity) : base(aggregateId, tenantId)
    {
        this.NewCapacity = newCapacity;
    }
       

    public override IDomainEvent WithAggregate(Guid aggregateId, Guid tenantId)
    {
        return new EventCapacityIncreasedDomainEvent(aggregateId, tenantId, this.NewCapacity);
    }
}