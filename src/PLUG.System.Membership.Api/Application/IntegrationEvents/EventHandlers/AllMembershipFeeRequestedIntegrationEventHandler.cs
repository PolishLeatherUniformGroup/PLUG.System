using MediatR;
using PLUG.System.EventBus.Abstraction;
using PLUG.System.IntegrationEvents;
using PLUG.System.Membership.Api.Application.Commands;
using PLUG.System.Membership.Api.Application.Queries;
using PLUG.System.SharedDomain;

namespace PLUG.System.Membership.Api.Application.IntegrationEvents.EventHandlers;

public class AllMembershipFeeRequestedIntegrationEventHandler : IIntegrationEventHandler<AllMembershipFeeRequestedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public AllMembershipFeeRequestedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(AllMembershipFeeRequestedIntegrationEvent @event)
    {
        var feeAmount = new Money(@event.Amount, @event.Currency);
        int page = 0;
        int pageSize = 20;
        bool hasMore = false;
        do
        {
            var query = new GetAllActiveRegularMembersQuery(page, pageSize);
            var result = await this._mediator.Send(query);
            foreach (var memberIdResult in result.Value)
            {
                var command = new RequestMemberFeePaymentCommand(memberIdResult.Id,
                    feeAmount, @event.DueDate, @event.Period);
                _ =await this._mediator.Send(command);
            }
            hasMore = result.Total > page * pageSize;
            page++;
        } while (hasMore);
    }
}