using Microsoft.AspNetCore.Mvc;

namespace ONPA.Organizations.Contract.Requests;

public record GetOrganizationFeesRequest([FromRoute]Guid OrganizationId,[FromQuery] int Page = 0, [FromQuery] int Limit = 10)
{
    public string ToQueryString()
    {
        var queryString = $"{OrganizationId}/fees?Page={this.Page}&Limit={this.Limit}";
        return queryString;
    }
}

public record GetOrganizationFeeForYearRequest([FromRoute] Guid OrganizationId, [FromRoute] int Year)
{
    public string ToQueryString()
    {
        var queryString = $"{OrganizationId}/fees/{Year}";
        return queryString;
    }

}