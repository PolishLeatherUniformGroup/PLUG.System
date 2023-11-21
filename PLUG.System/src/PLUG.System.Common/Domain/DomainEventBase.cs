using MediatR;

namespace PLUG.System.Common.Domain;

public abstract class DomainEventBase :
    INotification,
    IDomainEvent,
    IEquatable<DomainEventBase>
{
    protected DomainEventBase()
    {
        this.EventId = Guid.NewGuid();
        this.Timestamp = DateTime.UtcNow;
    }

    protected DomainEventBase(Guid aggregateId) : this()
    {
        this.AggregateId = aggregateId;
    }
    public Guid EventId { get; }
    public DateTime Timestamp { get; }
    public Guid AggregateId { get; }

    public virtual bool Equals(DomainEventBase? other)
    {
        return other != null &&
               this.EventId.Equals(other.EventId);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.EventId, this.Timestamp, this.AggregateId);
    }

    public abstract IDomainEvent WithAggregate(Guid aggregateId);
}