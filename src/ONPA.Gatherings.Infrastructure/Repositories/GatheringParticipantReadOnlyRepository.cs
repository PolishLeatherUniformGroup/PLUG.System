using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Gatherings.Infrastructure.Database;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Infrastructure.Repositories;

public sealed class GatheringParticipantReadOnlyRepository : IReadOnlyRepository<GatheringParticipant>
{
    private readonly GatheringsContext _dbContext;
    public async Task<GatheringParticipant?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._dbContext.GatheringParticipants
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<GatheringParticipant>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new())
    {
        return await this._dbContext.GatheringParticipants
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<GatheringParticipant>> ManyByFilter(Expression<Func<GatheringParticipant, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new())
    {
        return await this._dbContext.GatheringParticipants
            .AsNoTracking()
            .Where(filter)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}