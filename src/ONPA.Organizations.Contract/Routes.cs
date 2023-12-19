namespace ONPA.Organizations.Contract;

public static class Routes
{
    public const string Base = "api/organizations";
    public const string SingleOrganization = "{organizationId}";
    public const string SingleOrganizationFees = "{organizationId}/fees";
    public const string SingleOrganizationFeeForYear = "{organizationId}/fees/{year}";
    public const string SingleOrganizationSettings = "{organizationId}/settings";
}