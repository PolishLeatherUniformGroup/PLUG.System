using Microsoft.FluentUI.AspNetCore.Components;
using ONPA.WebApp.Data;

namespace ONPA.WebApp.Services.Abstractions
{
    public interface IMembershipService
    {
        Task<GridItemsProviderResult<MemberItem>> GetMembers(int status, int page = 0, int limit = 10);
        Task<bool> RegisterFeePayment(Guid memberId, Guid feeId, decimal paidAmount, string currency, DateTime period);
    }
}