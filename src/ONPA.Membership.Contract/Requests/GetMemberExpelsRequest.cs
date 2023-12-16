using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;

namespace ONPA.Membership.Contract.Requests;

public record GetMemberExpelsRequest([FromRoute] Guid MemberId, [FromQuery] int Page = 0, [FromQuery] int Limit = 10):MultiTenantRequest
{
    public string ToQueryString()
    {
        var queryString = $"{this.MemberId}/expels?Page={this.Page}&Limit={this.Limit}";
        return queryString;
    }
}