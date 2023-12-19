namespace ONPA.Common.Domain;

public interface IAggregateRepository<TAggregate> : IMultiTenantAggregateRepository<TAggregate> where TAggregate :  MultiTenantAggregateRoot
{
    Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = new());
    Task<TAggregate> CreateAsync(TAggregate aggregate, CancellationToken cancellationToken = new());
    Task<TAggregate> UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = new());
}

public interface IMultiTenantAggregateRepository<TAggregate> where TAggregate : MultiTenantAggregateRoot
{
    Task<TAggregate?> GetByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken = new());
    Task<TAggregate?> GetByIdAsyncForVersion(Guid tenantId, Guid id, long version, CancellationToken cancellationToken = new());
    Task<TAggregate> CreateAsync(TAggregate aggregate, CancellationToken cancellationToken = new());
    Task<TAggregate> UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = new());
}