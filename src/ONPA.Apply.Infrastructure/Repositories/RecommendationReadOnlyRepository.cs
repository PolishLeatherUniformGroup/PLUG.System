using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Apply.Infrastructure.Database;
using PLUG.System.Apply.Domain;
using ONPA.Common.Application;
using Recommendation = ONPA.Apply.Infrastructure.ReadModel.Recommendation;
using RecommendationRead = ONPA.Apply.Infrastructure.ReadModel.Recommendation;

namespace ONPA.Apply.Infrastructure.Repositories;

public class RecommendationReadOnlyRepository: IReadOnlyRepository<Recommendation>
{
    private readonly ApplyContext _context;

    public RecommendationReadOnlyRepository(ApplyContext context)
    {
        this._context = context;
    }

    public async Task<Recommendation?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._context.Recommendations.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Recommendation>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new())
    {
        return await this._context.Recommendations.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Recommendation>> ManyByFilter(Expression<Func<Recommendation, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new())
    {
        return await this._context.Recommendations
            .Where(filter)
            .Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}