using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Membership.Infrastructure.Database;
using ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Infrastructure.Repositories;

public sealed class MemberExpelReadOnlyRepository : IReadOnlyRepository<MemberExpel>
{
    private readonly MembershipContext _context;

    public MemberExpelReadOnlyRepository(MembershipContext context)
    {
        this._context = context;
    }

    public async Task<MemberExpel?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._context.MemberExpels.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<MemberExpel>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new())
    {
        return await this._context.MemberExpels.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<MemberExpel>> ManyByFilter(Expression<Func<MemberExpel, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new())
    {
        return await this._context.MemberExpels
            .Where(filter)
            .Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}