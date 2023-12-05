using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public record MembershipExtendedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public DateTime ValidUntil { get; init; }

    public MembershipExtendedIntegrationEvent(string firstName, string email, DateTime validUntil)
    {
        FirstName = firstName;
        Email = email;
        ValidUntil = validUntil;
    }
}