using PLUG.System.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using PLUG.System.Common.Application;
using PLUG.System.IntegrationEvents;

namespace PLUG.System.Apply.Api.Application.DomainEventHandlers;

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
        var integrationEvent = new ApplicationCancelledIntegrationEvent(notification.FirstName, notification.Email,
            Reason);
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}