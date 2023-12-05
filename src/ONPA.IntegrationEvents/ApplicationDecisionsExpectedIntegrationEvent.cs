using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

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