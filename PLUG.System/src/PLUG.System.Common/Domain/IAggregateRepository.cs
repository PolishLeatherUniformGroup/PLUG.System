namespace PLUG.System.Common.Domain;

public interface IAggregateRepository<TAggregate>
{
    Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = new());
    Task<TAggregate> CreateAsync(TAggregate aggregate, CancellationToken cancellationToken = new());
    Task<TAggregate> UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = new());
}