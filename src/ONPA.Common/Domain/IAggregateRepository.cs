namespace ONPA.Common.Domain;

public interface IAggregateRepository<TAggregate> : IMultiTenantAggregateRepository<TAggregate> where TAggregate :  MultiTenantAggregateRoot
{
    Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = new());
}