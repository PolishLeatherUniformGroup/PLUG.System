using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public record ApplicationDecisionsExpectedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
 
    public ApplicationDecisionsExpectedIntegrationEvent(Guid tenantId,string firstName, string email): base(tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
    }
}