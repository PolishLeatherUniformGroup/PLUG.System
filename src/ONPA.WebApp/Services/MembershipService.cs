using Flurl;
using Microsoft.FluentUI.AspNetCore.Components;
using ONPA.Apply.Contract.Requests;
using ONPA.Apply.Contract.Responses;
using ONPA.Common.Queries;
using ONPA.WebApp.Data;

namespace ONPA.WebApp.Services;

public class MembershipService
{
    private readonly HttpClient httpClient;

    public MembershipService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    
}

public class ApplyService
{
    private readonly HttpClient httpClient;

    public ApplyService(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<GridItemsProviderResult<ApplicationItem>> GetApplications(int status, int page = 0, int limit =10)
    {
        var request = new GetApplicationsRequest(status, page, limit);
        try
        {
            var apiUri = new Uri(Url.Combine(this.httpClient.BaseAddress.OriginalString, request.ToQueryString()));
            var httpResponse = await this.httpClient.GetAsync(apiUri);
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadFromJsonAsync<PageableResult<ApplicationResult>>();
            var applicationItems = result.Result.Select(x => new ApplicationItem()
            {
                Id = x.ApplicationId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Status = (ApplicationStatus)x.Status,
                ApplicationDate = x.ApplicationDate
            }).ToList();
           return GridItemsProviderResult.From(applicationItems, result.Total);
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}