using MediatR;
using PLUG.System.EventBus.Abstraction;
using PLUG.System.IntegrationEvents;
using PLUG.System.Membership.Api.Application.Commands;
using PLUG.System.SharedDomain;

namespace PLUG.System.Membership.Api.Application.IntegrationEvents.EventHandlers;

public class MemberJoinedIntegrationEventHandler : IIntegrationEventHandler<MemberJoinedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public MemberJoinedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(MemberJoinedIntegrationEvent @event)
    {
        var command = new CreateMemberCommand(
            @event.FirstName, @event.LastName, @event.Email, @event.Phone, @event.Address,
            @event.JoinDate, new Money(@event.PaidFeeAmount, @event.PaidFeeCurrency));
        
        _ = await this._mediator.Send(command);
    }
}