using Microsoft.FluentUI.AspNetCore.Components;
using ONPA.Apply.Contract.Responses;
using ONPA.WebApp.Data;

namespace ONPA.WebApp.Services.Abstractions
{
    public interface IApplyService
    {
        Task<bool> AcceptApplicationRejectionAppeal(Guid applicationId, DateTime acceptDate, string justification);
        Task<bool> ApproveApplication(Guid applicationId);
        Task<bool> DismissApplicationRejectionAppeal(Guid applicationId, DateTime rejectionDate, string justification);
        Task<ApplicationDetails> GetApplication(Guid applicationId);
        Task<GridItemsProviderResult<ApplicationItem>> GetApplications(int status, int page = 0, int limit = 10);
        Task<bool> RegisterApplicationFeePayment(Guid applicationId, decimal paidAmmount, string currency);
        Task<bool> RegisterApplicationRejectionAppeal(Guid applicationId, DateTime appealDate, string justification);
        Task<bool> RejectApplication(Guid applicationId, string decision, int days = 14);
    }
}