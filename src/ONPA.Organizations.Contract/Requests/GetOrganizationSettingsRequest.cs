﻿using Microsoft.AspNetCore.Mvc;

namespace ONPA.Organizations.Contract.Requests;

public record GetOrganizationSettingsRequest([FromRoute]Guid OrganizationId)
{
    public string ToQueryString()
    {
        var queryString = $"{this.OrganizationId}/settings";
        return queryString;
    }
}