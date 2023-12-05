using ONPA.Common.Application;
using ONPA.Membership.Api.Application.Queries.Results;

namespace ONPA.Membership.Api.Application.Queries;

public record ValidateMemberNumbersQuery : ApplicationQueryBase<SingleResult<MemberValidationResult>>
{
    public string MemberNumber { get; init; }

    public ValidateMemberNumbersQuery(string memberNumber)
        =>this.MemberNumber = memberNumber;
    
}