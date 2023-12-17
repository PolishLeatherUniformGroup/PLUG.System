using ONPA.Common.Application;
using ONPA.Membership.Contract.Responses;

namespace ONPA.Membership.Api.Application.Queries;

public record ValidateMemberNumberQuery : ApplicationQueryBase<MemberValidationResult>
{
    public string MemberNumber { get; init; }

    public ValidateMemberNumberQuery(Guid tenantId,string memberNumber): base(tenantId)
        =>this.MemberNumber = memberNumber;
    
}