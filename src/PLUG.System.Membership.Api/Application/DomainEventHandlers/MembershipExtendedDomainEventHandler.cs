using PLUG.System.Common.Application;
using PLUG.System.IntegrationEvents;
using PLUG.System.Membership.Api.Application.IntegrationEvents;
using PLUG.System.Membership.DomainEvents;
using PLUG.System.Membership.Infrastructure.Database;

namespace PLUG.System.Membership.Api.Application.DomainEventHandlers;

public class MembershipExtendedDomainEventHandler 
    : DomainEventHandlerBase<MembershipExtendedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public MembershipExtendedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        _integrationEventService = integrationEventService;
    }

    public override async Task Handle(MembershipExtendedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new MembershipExtendedIntegrationEvent(notification.FirstName,notification.Email,
            notification.ValidUntil);
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}