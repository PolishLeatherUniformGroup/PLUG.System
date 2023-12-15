using ONPA.Common.Domain;
using ONPA.Gatherings.Domain;
using ONPA.Gatherings.Infrastructure.Database;

namespace ONPA.Gatherings.Infrastructure.Repositories;

public class PublicGatheringAggregateRepository:IAggregateRepository<PublicGathering>
{
    private readonly GatheringsContext _context;

    public PublicGatheringAggregateRepository(GatheringsContext context)
    {
        this._context = context;
    }

    public async Task<PublicGathering?> GetByIdAsync(Guid id, CancellationToken cancellationToken = new ())
    {
        return await this._context.ReadAggregate<PublicGathering>(id, cancellationToken);
    }

    public async Task<PublicGathering> CreateAsync(PublicGathering aggregate, CancellationToken cancellationToken = new())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        return aggregate;
    }

    public async Task<PublicGathering> UpdateAsync(PublicGathering aggregate, CancellationToken cancellationToken = new())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        return aggregate;
    }
}