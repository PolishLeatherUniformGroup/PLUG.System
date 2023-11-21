using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEvents;

public record ApplicationReceivedIntegrationEvent :IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public Guid ApplicationId { get; init; }
    public string[] Recommenders { get; init; }

    public ApplicationReceivedIntegrationEvent(string firstName, string email, Guid applicationId, string[] recommenders)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ApplicationId = applicationId;
        this.Recommenders = recommenders;
    }
}