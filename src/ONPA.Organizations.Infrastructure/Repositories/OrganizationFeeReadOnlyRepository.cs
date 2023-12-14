using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Organizations.Infrastructure.Database;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Infrastructure.Repositories;

public sealed class OrganizationFeeReadOnlyRepository : IReadOnlyRepository<OrganizationFee>
{
    private readonly OrganizationsContext _context;

    public OrganizationFeeReadOnlyRepository(OrganizationsContext context)
    {
        this._context = context;
    }

    public async Task<OrganizationFee?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._context.OrganizationFees.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<OrganizationFee>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new CancellationToken())
    {
        return await this._context.OrganizationFees.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<OrganizationFee>> ManyByFilter(Expression<Func<OrganizationFee, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return await this._context.OrganizationFees
            .Where(filter)
            .Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}