using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PLUG.System.Apply.Infrastructure.Database;
using PLUG.System.Apply.Infrastructure.ReadModel;
using PLUG.System.Common.Application;

namespace PLUG.System.Apply.Infrastructure.Repositories;

public class ApplicationFormReadOnlyRepository: IReadOnlyRepository<ApplicationForm>
{
    private readonly ApplyContext _context;

    public ApplicationFormReadOnlyRepository(ApplyContext context)
    {
        this._context = context;
    }

    public async Task<ApplicationForm?> ReadSingleById(Guid id, CancellationToken cancellationToken)
    {
        return await this._context.ApplicationForms.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<ApplicationForm>> ReadMany(int page = 0, int pageSize = 20, CancellationToken cancellationToken = new CancellationToken())
    {
        return await this._context.ApplicationForms.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ApplicationForm>> ManyByFilter(Expression<Func<ApplicationForm, bool>> filter,
        int page = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return await this._context.ApplicationForms
            .Where(filter)
            .Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
}