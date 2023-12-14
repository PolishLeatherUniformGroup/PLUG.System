using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Organizations.Infrastructure.Database;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Infrastructure.Repositories;

public sealed class OrganizationReadOnlyRepository : IReadOnlyRepository<Organization>
{
    private readonly OrganizationsContext _context;

    public OrganizationReadOnlyRepository(OrganizationsContext context)
    {
        this._context = context;
    }

    public async Task<Organization?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._context.Organizations.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Organization>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new CancellationToken())
    {
        return await this._context.Organizations.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Organization>> ManyByFilter(Expression<Func<Organization, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return await this._context.Organizations
            .Where(filter)
            .Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}