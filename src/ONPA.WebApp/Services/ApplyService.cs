using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Flurl;
using Microsoft.FluentUI.AspNetCore.Components;
using ONPA.Apply.Contract;
using ONPA.Apply.Contract.Requests;
using ONPA.Apply.Contract.Responses;
using ONPA.Common.Queries;
using ONPA.WebApp.Data;

namespace ONPA.WebApp.Services;

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
    
    public async Task<ApplicationDetails> GetApplication(Guid applicationId)
    {
        var request = new GetApplicationRequest(applicationId);
        try
        {
            var apiUri = new Uri(Url.Combine(this.httpClient.BaseAddress.OriginalString, request.ToQueryString()));
            var httpResponse = await this.httpClient.GetAsync(apiUri);
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadFromJsonAsync<ApplicationDetails>();
            return result;
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    

    public async Task<bool> ApproveApplication(Guid applicationId)
    {
        var request = new ApproveApplicationRequest(applicationId, new ApplicationApproval(DateTime.UtcNow));
        try
        {
            
            var apiUri = new Uri(Url.Combine(this.httpClient.BaseAddress.OriginalString,Routes.SingleApplication)
                .Replace("{applicationId}",applicationId.ToString()));
            var payload = new StringContent(JsonSerializer.Serialize(request.Decision), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponse = await this.httpClient.PutAsync(apiUri, payload);
            httpResponse.EnsureSuccessStatusCode();
            return true;
        }catch(Exception)
        {
            return false;
        }
    }
    
    public async Task<bool> RejectApplication(Guid applicationId, string decision, int days=14)
    {
        var request = new RejectApplicationRequest(applicationId, new ApplicationRejection(DateTime.UtcNow, decision, days));
        try
        {
            
            var apiUri = new Uri(Url.Combine(this.httpClient.BaseAddress.OriginalString,Routes.SingleApplication)
                .Replace("{applicationId}",applicationId.ToString()));
            var payload = new StringContent(JsonSerializer.Serialize(request.Decision), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponse = await this.httpClient.PatchAsync(apiUri, payload);
            httpResponse.EnsureSuccessStatusCode();
            return true;
        }catch(Exception)
        {
            return false;
        }
    }
    
    public async Task<bool> RegisterApplicationFeePayment(Guid applicationId, decimal paidAmmount, string currency)
    {
        var request = new RegisterApplicationPaymentRequest(applicationId, new Payment(paidAmmount, currency));
        try
        {
            
            var apiUri = new Uri(Url.Combine(this.httpClient.BaseAddress.OriginalString,Routes.SingleApplicationPayments)
                .Replace("{applicationId}",applicationId.ToString()));
            var payload = new StringContent(JsonSerializer.Serialize(request.Payment), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponse = await this.httpClient.PostAsync(apiUri, payload);
            httpResponse.EnsureSuccessStatusCode();
            return true;
        }catch(Exception)
        {
            return false;
        }
    }
    
    public async Task<bool> RegisterApplicationRejectionAppeal(Guid applicationId, DateTime appealDate, string justification)
    {
        var request = new AppealApplicationRejectionRequest(applicationId, new (appealDate, justification));
        try
        {
            
            var apiUri = new Uri(Url.Combine(this.httpClient.BaseAddress.OriginalString,Routes.SingleApplicationAppeals)
                .Replace("{applicationId}",applicationId.ToString()));
            var payload = new StringContent(JsonSerializer.Serialize(request.Appeal), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponse = await this.httpClient.PostAsync(apiUri, payload);
            httpResponse.EnsureSuccessStatusCode();
            return true;
        }catch(Exception)
        {
            return false;
        }
    }
    
    public async Task<bool> AcceptApplicationRejectionAppeal(Guid applicationId, DateTime acceptDate, string justification)
    {
        var request = new ApproveAppealApplicationRejectionRequest(applicationId, new (acceptDate, justification));
        try
        {
            
            var apiUri = new Uri(Url.Combine(this.httpClient.BaseAddress.OriginalString,Routes.SingleApplicationAppeals)
                .Replace("{applicationId}",applicationId.ToString()));
            var payload = new StringContent(JsonSerializer.Serialize(request.Approval), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponse = await this.httpClient.PostAsync(apiUri, payload);
            httpResponse.EnsureSuccessStatusCode();
            return true;
        }catch(Exception)
        {
            return false;
        }
    }
    
    public async Task<bool> DismissApplicationRejectionAppeal(Guid applicationId, DateTime rejectionDate, string justification)
    {
        var request = new RejectAppealApplicationRejectionRequest(applicationId, new (rejectionDate, justification));
        try
        {
            
            var apiUri = new Uri(Url.Combine(this.httpClient.BaseAddress.OriginalString,Routes.SingleApplicationAppeals)
                .Replace("{applicationId}",applicationId.ToString()));
            var payload = new StringContent(JsonSerializer.Serialize(request.Rejection), Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpResponse = await this.httpClient.PostAsync(apiUri, payload);
            httpResponse.EnsureSuccessStatusCode();
            return true;
        }catch(Exception)
        {
            return false;
        }
    }
    
}