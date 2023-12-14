﻿using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record GetMembersRequest([FromQuery] int Status, [FromQuery] int Page = 0, [FromQuery] int Limit = 10)
{
    public string ToQueryString()
    {
        var queryString = $"?Status={this.Status}&Page={this.Page}&Limit={this.Limit}";
        return queryString;
    }
}