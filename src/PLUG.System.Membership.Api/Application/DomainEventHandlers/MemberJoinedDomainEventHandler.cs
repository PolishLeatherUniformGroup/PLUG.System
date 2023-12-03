using PLUG.System.Common.Application;
using PLUG.System.IntegrationEvents;
using PLUG.System.Membership.Api.Application.IntegrationEvents;
using PLUG.System.Membership.DomainEvents;

namespace PLUG.System.Membership.Api.Application.DomainEventHandlers;

public sealed class MemberJoinedDomainEventHandler :DomainEventHandlerBase<MemberJoinedDomainEvent>
{
    private readonly IIntegrationEventService _integrationEventService;

    public MemberJoinedDomainEventHandler(IIntegrationEventService integrationEventService)
    {
        _integrationEventService = integrationEventService;
    }

    public override async Task Handle(MemberJoinedDomainEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new MemberCardNumberAssignedIntegrationEvent(
            notification.CardNumber, notification.FirstName, notification.LastName,
            notification.Email,notification.Phone);
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}