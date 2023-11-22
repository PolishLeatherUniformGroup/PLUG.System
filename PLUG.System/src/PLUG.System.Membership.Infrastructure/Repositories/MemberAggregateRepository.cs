using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;
using PLUG.System.Membership.Infrastructure.Database;

namespace PLUG.System.Membership.Infrastructure.Repositories;

public sealed class MemberAggregateRepository : IAggregateRepository<Member>
{
    private readonly MembershipContext _context;

    public MemberAggregateRepository(MembershipContext context)
    {
        this._context = context;
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = new CancellationToken())
    {
        return await this._context.ReadAggregate<Member>(id, cancellationToken);
    }

    public async Task<Member> CreateAsync(Member aggregate, CancellationToken cancellationToken = new CancellationToken())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        // Fill ReadModel
        return aggregate;
    }

    public async Task<Member> UpdateAsync(Member aggregate, CancellationToken cancellationToken = new CancellationToken())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        // Fill ReadModel
        return aggregate;
    }
}