using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEvents;

public record ApplicationRejectionAppealDismissedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public DateTime RejectionDate { get; init; }
    public string DecisionDetail { get; init; }

    public ApplicationRejectionAppealDismissedIntegrationEvent(string firstName, string email, DateTime rejectionDate,
        string decisionDetail)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RejectionDate = rejectionDate;
        this.DecisionDetail = decisionDetail;
    }
}