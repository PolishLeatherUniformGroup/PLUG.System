using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;

namespace ONPA.Apply.Contract.Requests;

public record GetApplicationsRequest([FromQuery] int Status, [FromQuery] int Page = 0, [FromQuery] int Limit = 10):MultiTenantRequest
{
    public string ToQueryString()
    {
        var queryString = $"{Routes.Base}?Status={this.Status}&Page={this.Page}&Limit={this.Limit}";
        return queryString;
    }
}