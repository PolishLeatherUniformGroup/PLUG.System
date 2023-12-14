using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Membership.Infrastructure.Database;
using ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Infrastructure.Repositories;

public sealed class MemberReadOnlyRepository : IReadOnlyRepository<Member>
{
    private readonly MembershipContext _context;

    public MemberReadOnlyRepository(MembershipContext context)
    {
        this._context = context;
    }

    public async Task<Member?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._context.Members.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Member>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new())
    {
        return await this._context.Members.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Member>> ManyByFilter(Expression<Func<Member, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new())
    {
        return await this._context.Members
            .Where(filter)
            .Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}