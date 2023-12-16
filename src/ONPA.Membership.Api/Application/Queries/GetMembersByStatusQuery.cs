using System.Linq.Expressions;
using ONPA.Common.Application;
using ONPA.Membership.Contract.Responses;
using ONPA.Membership.Infrastructure.ReadModel;

namespace ONPA.Membership.Api.Application.Queries;

public record GetMembersByStatusQuery : ApplicationCollectionQueryBase<MemberResult>
{
    public int Status { get; init; }
    public GetMembersByStatusQuery(Guid tenantId,int status, int page, int limit) : base(tenantId,page, limit)
    {
        this.Status = status;
    }

    public Expression<Func<Member, bool>> AsFilter()
    {
        if (Status >= 0)
        {
            return x => x.Status == (MembershipStatus)this.Status;
        }

        return x => true;
    }
}