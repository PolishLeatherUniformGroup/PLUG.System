using Microsoft.AspNetCore.Mvc;

namespace ONPA.Organizations.Contract.Requests;

public record GetOrganizationsRequest([FromQuery] int Page = 0, [FromQuery] int Limit = 10)
{
    public string ToQueryString()
    {
        var queryString = $"{Routes.Base}?Page={this.Page}&Limit={this.Limit}";
        return queryString;
    }
}