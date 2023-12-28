using Flurl;
using ONPA.Common.Queries;
using ONPA.Organizations.Contract.Requests;
using ONPA.Organizations.Contract.Responses;
using ONPA.WebApp.Services.Abstractions;

namespace ONPA.WebApp.Services;

public class OrganizationService : IOrganizationService
{
    private readonly HttpClient _httpClient;
    
    public OrganizationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<IEnumerable<OrganizationResponse>> GetOrganizations(int page = 0, int limit = 10)
    {
        var request = new GetOrganizationsRequest(page, limit);
        try
        {
            var apiUri = new Uri(Url.Combine(_httpClient.BaseAddress.OriginalString, request.ToQueryString()));
            using var httpResponse = await _httpClient.GetAsync(apiUri);
            httpResponse.EnsureSuccessStatusCode();
            var result =await httpResponse.Content.ReadFromJsonAsync<PageableResult<OrganizationResponse>>();
            
            return result?.Result??Enumerable.Empty<OrganizationResponse>();
        }catch(Exception)
        {
            return Enumerable.Empty<OrganizationResponse>();
        }
    }

    public async Task<OrganizationSettingsResponse?> GetOrganizationSettings(Guid organizationId)
    {
        var request = new GetOrganizationSettingsRequest(organizationId);
        try
        {
            var apiUri = new Uri(Url.Combine(_httpClient.BaseAddress.OriginalString, request.ToQueryString()));
            using var httpResponse = await _httpClient.GetAsync(apiUri);
            httpResponse.EnsureSuccessStatusCode();
            var result =await httpResponse.Content.ReadFromJsonAsync<OrganizationSettingsResponse>();
            return result ?? default;
        }
        catch (Exception)
        {
            return default;
        }
    }
}