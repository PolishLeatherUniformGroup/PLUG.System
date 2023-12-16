using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;

namespace ONPA.Gatherings.Contract.Requests;

public record GetEventsByStatusRequest([FromQuery]int Status,[FromQuery] int Page,[FromQuery] int Limit): MultiTenantRequest
{
    public string ToQueryString()
    {
        return $"?status={this.Status}&page={this.Page}&limit={this.Limit}";
    }
}