using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEvents;

public record ApplicationRejectionAppealApprovedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    
    public DateTime ApproveDate { get; init; }

    public ApplicationRejectionAppealApprovedIntegrationEvent(string firstName, string email, DateTime approveDate)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ApproveDate = approveDate;
    }
}