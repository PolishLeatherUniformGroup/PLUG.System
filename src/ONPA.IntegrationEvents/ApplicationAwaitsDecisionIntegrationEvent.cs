using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public record ApplicationAwaitsDecisionIntegrationEvent : IntegrationEvent
{
    public Guid ApplicationId { get; init; }
    public string FirstName { get; init; }
    public string Email { get; init; }

    public ApplicationAwaitsDecisionIntegrationEvent(Guid tenantId,Guid applicationId, string firstName, string email): base(tenantId)
    {
        this.ApplicationId = applicationId;
        this.FirstName = firstName;
        this.Email = email;
    }
}