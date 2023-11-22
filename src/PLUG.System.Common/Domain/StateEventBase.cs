namespace PLUG.System.Common.Domain;

public abstract class StateEventBase:IStateEvent, IEquatable<StateEventBase>
{
    protected StateEventBase()
    {
        this.EventId = Guid.NewGuid();
        this.Timestamp = DateTime.UtcNow;
    }
    
    protected StateEventBase(Guid aggregateId, long aggregateVersion):this()
    {
        this.AggregateId = aggregateId;
        this.AggregateVersion = aggregateVersion;
    }
    
    public Guid EventId { get; }
    public Guid AggregateId { get; }
    public long AggregateVersion { get; }
    public DateTime Timestamp { get; }
    
    public bool Equals(StateEventBase? other)
    {
        return other != null && this.EventId.Equals(other.EventId);
    }

    public abstract IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion);
}