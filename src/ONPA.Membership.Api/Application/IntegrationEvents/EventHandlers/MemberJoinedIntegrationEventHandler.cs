using MediatR;
using ONPA.EventBus.Abstraction;
using ONPA.IntegrationEvents;
using ONPA.Membership.Api.Application.Commands;
using PLUG.System.SharedDomain;

namespace ONPA.Membership.Api.Application.IntegrationEvents.EventHandlers;

public class MemberJoinedIntegrationEventHandler : IIntegrationEventHandler<MemberJoinedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public MemberJoinedIntegrationEventHandler(IMediator mediator)
    {
        this._mediator = mediator;
    }

    public async Task Handle(MemberJoinedIntegrationEvent @event)
    {
        var command = new CreateMemberCommand(@event.TenantId,
            @event.FirstName, @event.LastName, @event.Email, @event.Phone, @event.Address,
            @event.JoinDate, new Money(@event.PaidFeeAmount, @event.PaidFeeCurrency));
        
        _ = await this._mediator.Send(command);
    }
}