using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEvents;

public record ApplicationAwaitsDecisionIntegrationEvent : IntegrationEvent
{
    public Guid ApplicationId { get; init; }
    public string FirstName { get; init; }
    public string Email { get; init; }

    public ApplicationAwaitsDecisionIntegrationEvent(Guid applicationId, string firstName, string email)
    {
        this.ApplicationId = applicationId;
        this.FirstName = firstName;
        this.Email = email;
    }
}