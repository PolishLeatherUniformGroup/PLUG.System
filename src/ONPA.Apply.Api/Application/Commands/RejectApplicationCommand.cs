using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record RejectApplicationCommand: ApplicationCommandBase
{
    public Guid ApplicationId { get; init; }
    public DateTime RejectionDate { get; init; }
    public string DecisionDetail { get; init; }
    public int DaysToAppeal { get; init; }

    public RejectApplicationCommand(Guid applicationId, DateTime rejectionDate, string decisionDetail,
        int daysToAppeal, Guid tenantId):base(tenantId)
    {
        this.ApplicationId = applicationId;
        this.RejectionDate = rejectionDate;
        this.DecisionDetail = decisionDetail;
        this.DaysToAppeal = daysToAppeal;
    }
}