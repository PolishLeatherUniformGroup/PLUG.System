using ONPA.Common.Exceptions;

namespace ONPA.Common.Domain;

public abstract class AggregateRoot : MultiTenantAggregateRoot
{
    protected AggregateRoot() :base(Guid.Empty) { }
    protected AggregateRoot(Guid id, IEnumerable<IStateEvent> changes) : base(id, Guid.Empty, changes) { }
}

public abstract class MultiTenantAggregateRoot : IAggregateRoot
{
    public const long NewAggregateVersion = 0;
    
    protected readonly ICollection<IDomainEvent> domainEvents = new LinkedList<IDomainEvent>();
    protected readonly ICollection<IStateEvent> stateEvents = new LinkedList<IStateEvent>();
    protected long version = NewAggregateVersion;

    protected MultiTenantAggregateRoot(Guid tenantId)
    {
        this.AggregateId = Guid.NewGuid();
        this.TenantId = tenantId;
    }
    
    protected MultiTenantAggregateRoot(Guid aggregateId, Guid tenantId, IEnumerable<IStateEvent> changes)
    {
        this.AggregateId = aggregateId;
        this.TenantId = tenantId;

        foreach (var change in changes)
        {
            this.stateEvents.Add(change);
        }
        this.ApplyStateChanges();
    }
    
    public Guid AggregateId
    {
        get;
        protected set;
    }
    
    public Guid TenantId
    {
        get;
        protected set;
    }
    
    public long Version
    {
        get => this.version;
        protected set => this.version = value;
    }
    
    public IEnumerable<IStateEvent> GetStateEvents()
    {
        return this.stateEvents.AsEnumerable();
    }

    public void ApplyStateChanges()
    {
        foreach (var change in this.stateEvents)
        {
            this.ApplyStateChange(change);
        }
    }

    public void ClearChanges()
    {
        this.stateEvents.Clear();
    }

    public IEnumerable<IDomainEvent> GetDomainEvents()
    {
        return this.domainEvents.AsEnumerable();
    }

    public void ClearDomainEvents()
    {
        this.domainEvents.Clear();
    }
    protected void RaiseChangeEvent<TChange>(TChange change) where TChange : IStateEvent
    {
        var changeWithAggregate = change.WithAggregate(this.AggregateId, this.Version);
        this.version++;
        this.stateEvents.Add(changeWithAggregate);
    }
    protected void RaiseDomainEvent<TEvent>(TEvent @event) where TEvent : DomainEventBase
    {
        var eventWithAggregate = @event.WithAggregate(this.AggregateId,this.TenantId);
        this.domainEvents.Add(eventWithAggregate);
    }
    
    protected void ApplyStateChange(IStateEvent change)
    {
        if (this.stateEvents.Any(c => Equals(c.EventId, @change.EventId)))
        {
            if (this.version != change.AggregateVersion)
            {
                throw new AggregateVersionMismatchException();
            }
            ((dynamic)this).ApplyChange((dynamic)change);
            this.version++;
        }
    }
}