using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEvents;

public record ApplicationRejectedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public string Reason { get; init; }
    public DateTime AppealDeadline { get; init; }

    public ApplicationRejectedIntegrationEvent(string firstName, string email, string reason,
        DateTime appealDeadline)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.Reason = reason;
        this.AppealDeadline = appealDeadline;
    }
}