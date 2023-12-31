using MediatR;
using ONPA.Common.Application;
using ONPA.EventBus.Abstraction;
using ONPA.IntegrationEvents;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Api.Application.Queries;
using ONPA.Membership.Contract.Responses;
using PLUG.System.SharedDomain;

namespace ONPA.Membership.Api.Application.IntegrationEvents.EventHandlers;

public class AllMembershipFeeRequestedIntegrationEventHandler : IIntegrationEventHandler<AllMembershipFeeRequestedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public AllMembershipFeeRequestedIntegrationEventHandler(IMediator mediator)
    {
        this._mediator = mediator;
    }

    public async Task Handle(AllMembershipFeeRequestedIntegrationEvent @event)
    {
        var feeAmount = new Money(@event.Amount, @event.Currency);
        int page = 0;
        int pageSize = 20;
        bool hasMore = false;
        do
        {
            var query = new GetAllActiveRegularMembersQuery(@event.TenantId,page, pageSize);
            var result = (CollectionResult<MemberIdResult>) await this._mediator.Send(query);
            foreach (var memberIdResult in result.Value)
            {
                var command = new RequestMemberFeePaymentCommand(@event.TenantId, memberIdResult.Id,
                    feeAmount, @event.DueDate, @event.Period);
                _ =await this._mediator.Send(command);
            }
            hasMore = result.Total > page * pageSize;
            page++;
        } while (!hasMore);
    }
}