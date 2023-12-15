using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Gatherings.Infrastructure.Database;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Infrastructure.Repositories;

public sealed class GatheringEnrollmentReadOnlyRepository : IReadOnlyRepository<GatheringEnrollment>
{
    private readonly GatheringsContext _dbContext;
    public async Task<GatheringEnrollment?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._dbContext.GatheringEnrollments
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<GatheringEnrollment>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new())
    {
        return await this._dbContext.GatheringEnrollments
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<GatheringEnrollment>> ManyByFilter(Expression<Func<GatheringEnrollment, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new())
    {
        return await this._dbContext.GatheringEnrollments
            .AsNoTracking()
            .Where(filter)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}