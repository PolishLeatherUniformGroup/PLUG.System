using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public record ApplicationRejectionAppealApprovedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    
    public DateTime ApproveDate { get; init; }

    public ApplicationRejectionAppealApprovedIntegrationEvent(Guid tenantId,string firstName, string email, DateTime approveDate): base(tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ApproveDate = approveDate;
    }
}