using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Membership.Infrastructure.Database;
using ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Infrastructure.Repositories;

public sealed class MemberSuspensionReadOnlyRepository : IReadOnlyRepository<MemberSuspension>
{
    private readonly MembershipContext _context;

    public MemberSuspensionReadOnlyRepository(MembershipContext context)
    {
        this._context = context;
    }

    public async Task<MemberSuspension?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._context.MemberSuspensions.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<MemberSuspension>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new())
    {
        return await this._context.MemberSuspensions.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<MemberSuspension>> ManyByFilter(Expression<Func<MemberSuspension, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new())
    {
        return await this._context.MemberSuspensions
            .Where(filter)
            .Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}