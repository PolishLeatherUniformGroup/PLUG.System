using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record GetMemberFeesRequest([FromRoute] Guid MemberId, [FromQuery] int Page = 0, [FromQuery] int Limit = 10)
{
    public string ToQueryString()
    {
        var queryString = $"{MemberId}/fees?Page={Page}&Limit={Limit}";
        return queryString;
    }
}