using Microsoft.AspNetCore.Mvc;

namespace ONPA.Organizations.Contract.Requests;

public record GetOrganizationFeesRequest([FromRoute]Guid OrganizationId,[FromQuery] int Page = 0, [FromQuery] int Limit = 10)
{
    public string ToQueryString()
    {
        var queryString = $"{this.OrganizationId}/fees?Page={this.Page}&Limit={this.Limit}";
        return queryString;
    }
}