using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record GetMemberSuspensionsRequest([FromRoute] Guid MemberId, [FromQuery] int Page = 0, [FromQuery] int Limit = 10)
{
    public string ToQueryString()
    {
        var queryString = $"{MemberId}/suspensions?Page={Page}&Limit={Limit}";
        return queryString;
    }
}