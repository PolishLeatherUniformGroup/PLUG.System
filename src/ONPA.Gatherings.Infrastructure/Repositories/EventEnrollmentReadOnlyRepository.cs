using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Gatherings.Infrastructure.Database;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Infrastructure.Repositories;

public sealed class EventEnrollmentReadOnlyRepository : IReadOnlyRepository<EventEnrollment>
{
    private readonly GatheringsContext _dbContext;
    public async Task<EventEnrollment?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._dbContext.EventEnrollments
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<EventEnrollment>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new())
    {
        return await this._dbContext.EventEnrollments
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<EventEnrollment>> ManyByFilter(Expression<Func<EventEnrollment, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new())
    {
        return await this._dbContext.EventEnrollments
            .AsNoTracking()
            .Where(filter)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}