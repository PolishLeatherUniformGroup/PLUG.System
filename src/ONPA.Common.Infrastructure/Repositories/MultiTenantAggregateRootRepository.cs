using ONPA.Common.Domain;

namespace ONPA.Common.Infrastructure.Repositories
{

    public class MultiTenantAggregateRootRepository<TContext, TAggregate> : IMultiTenantAggregateRepository<TAggregate> where TAggregate : MultiTenantAggregateRoot
        where TContext: StreamContext
    {
        protected readonly TContext _context;
        public IUnitOfWork UnitOfWork => this._context;

        public MultiTenantAggregateRootRepository(TContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TAggregate> CreateAsync(TAggregate aggregate, CancellationToken cancellationToken = default)
        {
            await this._context.StoreAggregate(aggregate, cancellationToken);
            await this.OnAggregateCreate(aggregate);
            return aggregate;
        }

        public async Task<TAggregate?> GetByIdAsync(Guid tenantId, Guid id, CancellationToken cancellationToken = default)
        {
            return await this._context.ReadAggregate<TAggregate>(tenantId, id, cancellationToken);
        }

        public Task<TAggregate?> GetByIdAsyncForVersion(Guid tenantId, Guid id, long version, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<TAggregate> UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken = default)
        {
            await this._context.StoreAggregate(aggregate, cancellationToken);
            await this.OnAggregateCreate(aggregate);
            return aggregate;
        }

        protected virtual Task OnAggregateCreate(TAggregate aggregate)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnAggregateUpdate(TAggregate aggregate)
        {
            return Task.CompletedTask;
        }
    }
}
