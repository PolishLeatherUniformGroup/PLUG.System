using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record GetMemberExpelsRequest([FromRoute] Guid MemberId, [FromQuery] int Page = 0, [FromQuery] int Limit = 10)
{
    public string ToQueryString()
    {
        var queryString = $"{MemberId}/expels?Page={Page}&Limit={Limit}";
        return queryString;
    }
}