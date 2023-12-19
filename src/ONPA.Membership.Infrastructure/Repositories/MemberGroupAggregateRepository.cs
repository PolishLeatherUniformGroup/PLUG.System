using ONPA.Common.Domain;
using ONPA.Common.Infrastructure.Repositories;
using ONPA.Membership.Domain;
using ONPA.Membership.Infrastructure.Database;

namespace ONPA.Membership.Infrastructure.Repositories;

public sealed class MemberGroupAggregateRepository : MultiTenantAggregateRootRepository<MembershipContext,MembersGroup>
{

    public MemberGroupAggregateRepository(MembershipContext context):base(context)
    {
  
    }

}