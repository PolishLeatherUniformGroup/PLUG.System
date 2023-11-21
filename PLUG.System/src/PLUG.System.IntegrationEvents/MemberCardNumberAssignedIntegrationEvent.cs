using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEvents;

public sealed record MemberCardNumberAssignedIntegrationEvent : IntegrationEvent
{
    public string CardNumber { get; init; }
    public string FirstName { get; init; }
    public string Email { get; init; }

    public MemberCardNumberAssignedIntegrationEvent(string cardNumber, string firstName, string email)
    {
        CardNumber = cardNumber;
        FirstName = firstName;
        Email = email;
    }
}