using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Organizations.Infrastructure.Database;
using ONPA.Organizations.Infrastructure.ReadModel;

namespace ONPA.Organizations.Infrastructure.Repositories;

public sealed class OrganizationSettingsReadOnlyRepository : IReadOnlyRepository<OrganizationSettings>
{
    private readonly OrganizationsContext _context;

    public OrganizationSettingsReadOnlyRepository(OrganizationsContext context)
    {
        this._context = context;
    }

    public async Task<OrganizationSettings?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._context.OrganizationSettings.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<OrganizationSettings>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new())
    {
        return await this._context.OrganizationSettings.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<OrganizationSettings>> ManyByFilter(Expression<Func<OrganizationSettings, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new())
    {
        return await this._context.OrganizationSettings
            .Where(filter)
            .Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}