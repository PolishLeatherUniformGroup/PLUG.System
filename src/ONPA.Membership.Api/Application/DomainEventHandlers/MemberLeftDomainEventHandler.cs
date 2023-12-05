using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Membership.Api.Application.IntegrationEvents;
using ONPA.Membership.Domain;
using ONPA.Membership.DomainEvents;

namespace ONPA.Membership.Api.Application.DomainEventHandlers;

public sealed class MemberLeftDomainEventHandler : DomainEventHandlerBase<MemberLeftDomainEvent>
{
    private readonly IAggregateRepository<MembersGroup> _groupAggregateRepository;
    private readonly IIntegrationEventService _integrationEventService;

    public MemberLeftDomainEventHandler(
        IAggregateRepository<MembersGroup> groupAggregateRepository, 
        IIntegrationEventService integrationEventService)
    {
        this._groupAggregateRepository = groupAggregateRepository;
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(MemberLeftDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (var group in notification.Groups)
        {
            var groupAggregate = await this._groupAggregateRepository.GetByIdAsync(group, cancellationToken);
            if (groupAggregate is null)
            {
                continue;
            }
            groupAggregate.RemoveFromGroup(notification.CardNumber,notification.LeaveDate);
            await this._groupAggregateRepository.UpdateAsync(groupAggregate, cancellationToken);
        }
    }
}