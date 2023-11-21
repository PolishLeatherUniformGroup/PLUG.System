using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEvents;

public record ApplicationRejectionAppealReceivedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public DateTime AppealDate { get; init; }

    public ApplicationRejectionAppealReceivedIntegrationEvent(string firstName, string email, DateTime appealDate)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.AppealDate = appealDate;
    }
}