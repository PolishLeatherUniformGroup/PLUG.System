using Flurl;
using Microsoft.FluentUI.AspNetCore.Components;
using ONPA.Common.Queries;
using ONPA.Organizations.Contract.Requests;
using ONPA.Organizations.Contract.Responses;
using ONPA.WebApp.Data;
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

    public async Task<OrganizationSettingsData?> GetOrganizationSettings(Guid organizationId)
    {
        var request = new GetOrganizationSettingsRequest(organizationId);
        try
        {
            var apiUri = new Uri(Url.Combine(_httpClient.BaseAddress.OriginalString, request.ToQueryString()));
            using var httpResponse = await _httpClient.GetAsync(apiUri);
            httpResponse.EnsureSuccessStatusCode();
            var result =await httpResponse.Content.ReadFromJsonAsync<OrganizationSettingsResponse>();
            return new OrganizationSettingsData()
            {
                DaysToAppeal = result.DaysForAppeal,
                FeePaymentMonth = result.FeePaymentMonth,
                RequiredRecomendation = result.RequiredRecommendations,
            };
        }
        catch (Exception)
        {
            return default;
        }
    }

    public async Task UpdateOrganizationSettings(Guid organizationId, OrganizationSettingsData settings)
    {
        var request = new UpdateOrganizationSettingsRequest(organizationId, 
            new OrganizationSettings(settings.RequiredRecomendation,settings.DaysToAppeal,settings.FeePaymentMonth));
      
            var apiUri = new Uri(Url.Combine(_httpClient.BaseAddress.OriginalString, request.ToQueryString()));
            var response = await _httpClient.PutAsJsonAsync(apiUri, request);
            response.EnsureSuccessStatusCode();
    }
}