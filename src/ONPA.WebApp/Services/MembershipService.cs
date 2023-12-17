using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Flurl;
using Microsoft.FluentUI.AspNetCore.Components;
using ONPA.Common.Queries;
using ONPA.Membership.Contract;
using ONPA.Membership.Contract.Requests;
using ONPA.Membership.Contract.Requests.Dtos;
using ONPA.Membership.Contract.Responses;
using ONPA.WebApp.Data;

namespace ONPA.WebApp.Services;

public class MembershipService
{
    private readonly HttpClient httpClient;

    public MembershipService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }
    public async Task<GridItemsProviderResult<MemberItem>> GetMembers(int status, int page = 0, int limit =10)
    {
        var request = new GetMembersRequest(status, page, limit);
        try
        {
            var apiUri = new Uri(Url.Combine(this.httpClient.BaseAddress.OriginalString, request.ToQueryString()));
            using var httpResponse = await this.httpClient.GetAsync(apiUri);
            httpResponse.EnsureSuccessStatusCode();
            var result = await httpResponse.Content.ReadFromJsonAsync<PageableResult<MemberResult>>();
            var applicationItems = result.Result.Select(x => new MemberItem()
            {
                Id = x.MemberId,
                CardNumber = x.CardNumber,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Status = (MemberStatus)x.Status,
                JoinDate = x.JoinDate
            }).ToList();
            return GridItemsProviderResult.From(applicationItems, result.Total);
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    
    public async Task<bool> RegisterFeePayment(Guid memberId, Guid feeId, decimal paidAmount, string currency, DateTime period)
    {
        var request = new RegisterMembershipFeePaymentRequest(memberId,  feeId, new Payment(paidAmount, currency, period));
        try
        {
            
            var apiUri = new Uri(Url.Combine(this.httpClient.BaseAddress.OriginalString,Routes.SingleMemberFees)
                .Replace("{applicationId}",memberId.ToString()));
            using var payload = new StringContent(JsonSerializer.Serialize(request.Payment), Encoding.UTF8, MediaTypeNames.Application.Json);
            using var httpResponse = await this.httpClient.PostAsync(apiUri, payload);
            httpResponse.EnsureSuccessStatusCode();
            return true;
        }catch(Exception)
        {
            return false;
        }
    }
}