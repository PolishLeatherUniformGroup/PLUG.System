using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Gatherings.Infrastructure.Database;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Infrastructure.Repositories;

public sealed class PublicGatheringReadOnlyRepository : IReadOnlyRepository<PublicGathering>
{
    private readonly GatheringsContext _dbContext;
    public async Task<PublicGathering?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._dbContext.PublicGatherings
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<PublicGathering>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new())
    {
        return await this._dbContext.PublicGatherings
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PublicGathering>> ManyByFilter(Expression<Func<PublicGathering, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new())
    {
        return await this._dbContext.PublicGatherings
            .AsNoTracking()
            .Where(filter)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}