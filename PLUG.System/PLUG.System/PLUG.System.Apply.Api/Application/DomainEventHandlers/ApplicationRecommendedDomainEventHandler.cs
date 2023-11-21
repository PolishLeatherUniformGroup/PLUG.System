using PLUG.System.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using PLUG.System.Common.Application;
using PLUG.System.IntegrationEvents;

namespace PLUG.System.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationRecommendedDomainEventHandler : DomainEventHandlerBase<ApplicationRecommendedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public ApplicationRecommendedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationRecommendedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ApplicationAwaitsDecisionIntegrationEvent(notification.AggregateId,
            notification.FirstName, notification.LastName);
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}