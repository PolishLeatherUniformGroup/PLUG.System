using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PLUG.System.Apply.Domain;
using PLUG.System.Apply.Infrastructure.Database;
using PLUG.System.Common.Application;
using RecommendationRead = PLUG.System.Apply.Infrastructure.ReadModel.Recommendation;

namespace PLUG.System.Apply.Infrastructure.Repositories;

public class RecommendationReadOnlyRepository: IReadOnlyRepository<RecommendationRead>
{
    private readonly ApplyContext _context;

    public RecommendationReadOnlyRepository(ApplyContext context)
    {
        this._context = context;
    }

    public async Task<RecommendationRead?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._context.Recommendations.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<RecommendationRead>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new CancellationToken())
    {
        return await this._context.Recommendations.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<RecommendationRead>> ManyByFilter(Expression<Func<RecommendationRead, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return await this._context.Recommendations
            .Where(filter)
            .Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}