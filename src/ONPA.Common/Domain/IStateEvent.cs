namespace ONPA.Common.Domain;

public interface IStateEvent
{
    Guid EventId { get; }
    Guid AggregateId { get; }
    Guid TenantId { get; }
    long AggregateVersion { get; }
    DateTime Timestamp { get; }
    IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion);
}