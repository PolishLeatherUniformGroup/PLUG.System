using ONPA.Common.Domain;

namespace ONPA.Common.Infrastructure.Repositories
{
    public class AggregateRootRepository<TContext, TAggregate> : MultiTenantAggregateRootRepository<TContext, TAggregate>, IAggregateRepository<TAggregate>
        where TAggregate : AggregateRoot
        where TContext : StreamContext
    {
        public AggregateRootRepository(TContext context) : base(context)
        {
        }

        public Task<TAggregate?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => base.GetByIdAsync(Guid.Empty, id, cancellationToken);         
    }
}
