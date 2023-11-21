using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEvents;

public record ApplicationDecisionsExpectedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public DateTime ExpectedDecisionDate { get; init; }

    public ApplicationDecisionsExpectedIntegrationEvent(string firstName, string email, DateTime expectedDecisionDate)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ExpectedDecisionDate = expectedDecisionDate;
    }
}