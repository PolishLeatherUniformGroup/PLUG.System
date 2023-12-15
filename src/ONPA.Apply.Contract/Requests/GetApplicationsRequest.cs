using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Contract.Requests;

public record GetApplicationsRequest([FromQuery] int Status, [FromQuery] int Page = 0, [FromQuery] int Limit = 10)
{
    public string ToQueryString()
    {
        var queryString = $"?Status={this.Status}&Page={this.Page}&Limit={this.Limit}";
        return queryString;
    }
}