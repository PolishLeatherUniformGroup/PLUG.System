using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public sealed record MemberCardNumberAssignedIntegrationEvent : IntegrationEvent
{
    public string CardNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }

    public MemberCardNumberAssignedIntegrationEvent(Guid tenantId,string cardNumber, string firstName, string lastName,
        string email,
        string phone): base(tenantId)
    {
        this.CardNumber = cardNumber;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Phone = phone;
    }
}