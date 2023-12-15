using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Gatherings.Infrastructure.Database;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Infrastructure.Repositories;

public sealed class EventReadOnlyRepository : IReadOnlyRepository<Event>
{
    private readonly GatheringsContext _dbContext;
    public async Task<Event?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._dbContext.Events
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Event>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new())
    {
        return await this._dbContext.Events
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Event>> ManyByFilter(Expression<Func<Event, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new())
    {
        return await this._dbContext.Events
            .AsNoTracking()
            .Where(filter)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}