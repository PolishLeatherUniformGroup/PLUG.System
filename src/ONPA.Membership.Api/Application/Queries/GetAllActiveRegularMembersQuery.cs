using ONPA.Common.Application;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.Queries;

public record GetAllActiveRegularMembersQuery : ApplicationCollectionQueryBase<MemberIdResult>
{


    public GetAllActiveRegularMembersQuery(Guid tenantId,int page, int pageSize) : base(tenantId, page, pageSize)
    {
       
    }
}