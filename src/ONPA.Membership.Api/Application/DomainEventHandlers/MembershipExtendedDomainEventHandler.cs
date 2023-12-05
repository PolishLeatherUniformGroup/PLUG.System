using ONPA.Common.Application;
using ONPA.IntegrationEvents;
using ONPA.Membership.Api.Application.IntegrationEvents;
using ONPA.Membership.DomainEvents;
using ONPA.Membership.Infrastructure.Database;

namespace ONPA.Membership.Api.Application.DomainEventHandlers;

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