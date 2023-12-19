namespace ONPA.Common.Domain;

public abstract class AggregateRoot : MultiTenantAggregateRoot
{
    protected AggregateRoot() :base(Guid.Empty) { }
    protected AggregateRoot(Guid id, IEnumerable<IStateEvent> changes) : base(id, Guid.Empty, changes) { }
}
