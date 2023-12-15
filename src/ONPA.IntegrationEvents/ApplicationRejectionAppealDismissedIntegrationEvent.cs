using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public record ApplicationRejectionAppealDismissedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public DateTime RejectionDate { get; init; }
    public string DecisionDetail { get; init; }

    public ApplicationRejectionAppealDismissedIntegrationEvent(Guid tenantId,string firstName, string email, DateTime rejectionDate,
        string decisionDetail): base(tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RejectionDate = rejectionDate;
        this.DecisionDetail = decisionDetail;
    }
}