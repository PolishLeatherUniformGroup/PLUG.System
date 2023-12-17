using ONPA.Common.Domain;
using ONPA.Membership.Domain;
using ONPA.Membership.Infrastructure.Database;

namespace ONPA.Membership.Infrastructure.Repositories;

public sealed class MemberGroupAggregateRepository : IAggregateRepository<MembersGroup>
{
    private readonly MembershipContext _context;

    public MemberGroupAggregateRepository(MembershipContext context)
    {
        this._context = context;
    }

    public async Task<MembersGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken = new())
    {
        return await this._context.ReadAggregate<MembersGroup>(id, cancellationToken);
    }

    public async Task<MembersGroup> CreateAsync(MembersGroup aggregate, CancellationToken cancellationToken = new())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        // Fill ReadModel
        return aggregate;
    }

    public async Task<MembersGroup> UpdateAsync(MembersGroup aggregate, CancellationToken cancellationToken = new())
    {
        await this._context.StoreAggregate(aggregate, cancellationToken);
        // Fill ReadModel
        return aggregate;
    }
}