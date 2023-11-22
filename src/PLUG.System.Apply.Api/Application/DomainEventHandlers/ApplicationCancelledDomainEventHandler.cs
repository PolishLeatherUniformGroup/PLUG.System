using PLUG.System.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using PLUG.System.Common.Application;
using PLUG.System.IntegrationEvents;

namespace PLUG.System.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationCancelledDomainEventHandler : DomainEventHandlerBase<ApplicationCancelledDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public ApplicationCancelledDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationCancelledDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ApplicationCancelledIntegrationEvent(notification.FirstName, notification.Email,
            notification.Reason);
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}