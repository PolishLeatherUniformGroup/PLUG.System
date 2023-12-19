using ONPA.Common.Domain;
using ONPA.Common.Infrastructure.Repositories;
using ONPA.Membership.Domain;
using ONPA.Membership.Infrastructure.Database;

namespace ONPA.Membership.Infrastructure.Repositories;

public sealed class MemberAggregateRepository : MultiTenantAggregateRootRepository<MembershipContext,Member>
{ 

    public MemberAggregateRepository(MembershipContext context) :base(context)
    {
    }

}