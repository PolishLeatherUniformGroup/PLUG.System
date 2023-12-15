using ONPA.Common.Domain;
using ONPA.Gatherings.Domain;
using ONPA.Gatherings.Infrastructure.Database;

namespace ONPA.Gatherings.Infrastructure.Repositories;

public class PublicGatheringAggregateRepository:IAggregateRepository<Event>
{
    private readonly GatheringsContext _context;

    public PublicGatheringAggregateRepository(GatheringsContext context)
    {
        this._context = context;
    }

    public async Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken = new ())
    {
        return await this._context.ReadAggregate<Event>(id, cancellationToken);
    }

    public async Task<Event> CreateAsync(Event aggregate, CancellationToken cancellationToken = new())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        return aggregate;
    }

    public async Task<Event> UpdateAsync(Event aggregate, CancellationToken cancellationToken = new())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        return aggregate;
    }
}