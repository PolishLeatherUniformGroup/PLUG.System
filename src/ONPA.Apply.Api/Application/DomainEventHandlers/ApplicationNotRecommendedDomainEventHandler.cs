using ONPA.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using ONPA.Common.Application;
using ONPA.IntegrationEvents;

namespace ONPA.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationNotRecommendedDomainEventHandler : DomainEventHandlerBase<ApplicationNotRecommendedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;
    private const string Reason="Co najmniej jedna osoba wycofała swoją rekomendację.";

    public ApplicationNotRecommendedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationNotRecommendedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ApplicationCancelledIntegrationEvent(notification.TenantId,notification.FirstName, notification.Email,
            Reason);
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}