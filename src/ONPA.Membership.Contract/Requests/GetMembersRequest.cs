using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record GetMembersRequest([FromQuery] int Status, [FromQuery] int Page = 0, [FromQuery] int Limit = 10)
{
    public string ToQueryString()
    {
        var queryString = $"?Status={Status}&Page={Page}&Limit={Limit}";
        return queryString;
    }
}
public record GetMemberRequest([FromRoute] Guid MemberId);
public record GetMemberFeesRequest([FromRoute] Guid MemberId, [FromQuery] int Page = 0, [FromQuery] int Limit = 10)
{
    public string ToQueryString()
    {
        var queryString = $"{MemberId}/fees?Page={Page}&Limit={Limit}";
        return queryString;
    }
}

public record GetMemberSuspensionsRequest([FromRoute] Guid MemberId, [FromQuery] int Page = 0, [FromQuery] int Limit = 10)
{
    public string ToQueryString()
    {
        var queryString = $"{MemberId}/suspensions?Page={Page}&Limit={Limit}";
        return queryString;
    }
}
public record GetMemberExpelsRequest([FromRoute] Guid MemberId, [FromQuery] int Page = 0, [FromQuery] int Limit = 10)
{
    public string ToQueryString()
    {
        var queryString = $"{MemberId}/expels?Page={Page}&Limit={Limit}";
        return queryString;
    }
}