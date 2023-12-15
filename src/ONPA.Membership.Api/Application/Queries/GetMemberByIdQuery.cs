using ONPA.Common.Application;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.Queries;

public record GetMemberByIdQuery(Guid TenantId,Guid MemberId) : ApplicationQueryBase<MemberResult>(TenantId)
{
    
}