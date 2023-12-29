using ONPA.Organizations.Contract.Responses;
using ONPA.WebApp.Data;

namespace ONPA.WebApp.Services.Abstractions;

public interface IOrganizationService
{
    Task<IEnumerable<OrganizationResponse>> GetOrganizations(int page = 0, int limit = 10);
    
    Task<OrganizationSettingsData?> GetOrganizationSettings(Guid organizationId);
    
    Task UpdateOrganizationSettings(Guid organizationId, OrganizationSettingsData settings);
}