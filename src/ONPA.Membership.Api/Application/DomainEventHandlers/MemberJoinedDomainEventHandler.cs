using ONPA.Common.Application;
using ONPA.IntegrationEvents;
using ONPA.Membership.Api.Application.IntegrationEvents;
using ONPA.Membership.DomainEvents;

namespace ONPA.Membership.Api.Application.DomainEventHandlers;

public sealed class MemberJoinedDomainEventHandler :DomainEventHandlerBase<MemberJoinedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public MemberJoinedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(MemberJoinedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new MemberCardNumberAssignedIntegrationEvent(notification.TenantId,
            notification.CardNumber, notification.FirstName, notification.LastName,
            notification.Email,notification.Phone);
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}