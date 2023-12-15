using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public record MembershipExtendedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public DateTime ValidUntil { get; init; }

    public MembershipExtendedIntegrationEvent(Guid tenantId,string firstName, string email, DateTime validUntil): base(tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ValidUntil = validUntil;
    }
}