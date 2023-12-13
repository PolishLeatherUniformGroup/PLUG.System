using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Contract.Requests;

public record GetApplicationRequest([FromRoute] Guid ApplicationId)
{
    public string ToQueryString()
    {
        var queryString = $"/{ApplicationId}";
        return queryString;
    }
}