namespace PLUG.System.Apply.Api.Services;

public interface IIdentityService
{
    string GetUserIdentity();

    string GetUserName();
}

public class IdentityService(IHttpContextAccessor context) : IIdentityService
{
    public string GetUserIdentity()
        => context.HttpContext?.User.FindFirst("sub")?.Value;

    public string GetUserName()
        => context.HttpContext?.User.Identity?.Name;
}