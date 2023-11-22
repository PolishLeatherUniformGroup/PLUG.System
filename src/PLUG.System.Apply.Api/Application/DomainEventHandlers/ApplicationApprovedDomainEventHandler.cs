using PLUG.System.Apply.Api.Application.IntegrationEvents;
using PLUG.System.Apply.DomainEvents;
using PLUG.System.Common.Application;
using PLUG.System.IntegrationEvents;

namespace PLUG.System.Apply.Api.Application.DomainEventHandlers;

public sealed class ApplicationApprovedDomainEventHandler : DomainEventHandlerBase<ApplicationApprovedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public ApplicationApprovedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(ApplicationApprovedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new MemberJoinedIntegrationEvent(notification.FirstName,notification.LastName,notification.Email,
            notification.Phone, notification.Address,notification.ApproveDate, notification.PaidFee. Amount,notification.PaidFee.Currency);
        
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}