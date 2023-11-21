namespace PLUG.System.Common.Domain;

public interface IDomainEvent
{
    Guid EventId { get; }
    Guid AggregateId { get; }
    DateTime Timestamp { get; }
}