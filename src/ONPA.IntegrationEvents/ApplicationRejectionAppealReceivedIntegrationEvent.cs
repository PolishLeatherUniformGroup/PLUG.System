using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public record ApplicationRejectionAppealReceivedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public DateTime AppealDate { get; init; }

    public ApplicationRejectionAppealReceivedIntegrationEvent(Guid tenantId,string firstName, string email, DateTime appealDate): base(tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.AppealDate = appealDate;
    }
}