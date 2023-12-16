using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;

namespace ONPA.Apply.Contract.Requests;

public record GetApplicationRequest([FromRoute] Guid ApplicationId):MultiTenantRequest
{
    public string ToQueryString()
    {
        var queryString = $"/{this.ApplicationId}";
        return queryString;
    }
}