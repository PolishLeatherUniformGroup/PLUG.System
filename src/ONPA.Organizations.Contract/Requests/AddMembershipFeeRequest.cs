using Microsoft.AspNetCore.Mvc;

namespace ONPA.Organizations.Contract.Requests;

public sealed record AddMembershipFeeRequest([FromRoute]Guid OrganizationId,[FromBody]MembershipFee Fee)
{
}