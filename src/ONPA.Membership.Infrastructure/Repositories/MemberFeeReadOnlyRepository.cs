using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Membership.Infrastructure.Database;
using ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Infrastructure.Repositories;

public sealed class MemberFeeReadOnlyRepository : IReadOnlyRepository<MemberFee>
{
    private readonly MembershipContext _context;

    public MemberFeeReadOnlyRepository(MembershipContext context)
    {
        this._context = context;
    }

    public async Task<MemberFee?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._context.MemberFees.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<MemberFee>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new())
    {
        return await this._context.MemberFees.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<MemberFee>> ManyByFilter(Expression<Func<MemberFee, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new())
    {
        return await this._context.MemberFees
            .Where(filter)
            .Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}