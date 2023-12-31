﻿namespace ONPA.Apply.Api.Services;

public class IdentityService(IHttpContextAccessor context) : IIdentityService
{
    public string GetUserIdentity()
        => context.HttpContext?.User.FindFirst("sub")?.Value;

    public string GetUserName()
        => context.HttpContext?.User.Identity?.Name;

    public Guid GetUserOrganization()
    {
        throw new NotImplementedException();
    }
}