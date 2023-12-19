using ONPA.Organizations.Contract.Responses;

namespace ONPA.WebApp.Services.Abstractions;

public interface IOrganizationService
{
    Task<IEnumerable<OrganizationResponse>> GetOrganizations(int page = 0, int limit = 10);
}