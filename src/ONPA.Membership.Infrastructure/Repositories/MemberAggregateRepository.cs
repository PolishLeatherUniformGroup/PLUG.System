using ONPA.Common.Domain;
using ONPA.Membership.Domain;
using ONPA.Membership.Infrastructure.Database;

namespace ONPA.Membership.Infrastructure.Repositories;

public sealed class MemberAggregateRepository : IAggregateRepository<Member>
{
    private readonly MembershipContext _context;

    public MemberAggregateRepository(MembershipContext context)
    {
        this._context = context;
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = new())
    {
        return await this._context.ReadAggregate<Member>(id, cancellationToken);
    }

    public async Task<Member> CreateAsync(Member aggregate, CancellationToken cancellationToken = new())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        // Fill ReadModel
        return aggregate;
    }

    public async Task<Member> UpdateAsync(Member aggregate, CancellationToken cancellationToken = new())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        // Fill ReadModel
        return aggregate;
    }
}