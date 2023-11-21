using PLUG.System.Common.Application;
using PLUG.System.Membership.Api.Application.Queries.Results;

namespace PLUG.System.Membership.Api.Application.Queries;

public record ValidateMemberNumbersQuery : ApplicationQueryBase<SingleResult<MemberValidationResult>>
{
    public string MemberNumber { get; init; }

    public ValidateMemberNumbersQuery(string memberNumber)
        =>this.MemberNumber = memberNumber;
    
}