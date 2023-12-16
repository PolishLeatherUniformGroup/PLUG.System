namespace ONPA.Membership.Api.Services;

public interface IIdentityService
{
    string GetUserIdentity();
    string GetUserName();
    Guid GetUserOrganization();
}