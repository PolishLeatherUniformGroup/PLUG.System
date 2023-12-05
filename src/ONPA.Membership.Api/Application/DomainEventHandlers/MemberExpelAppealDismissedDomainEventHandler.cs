using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Membership.Api.Application.IntegrationEvents;
using ONPA.Membership.Domain;
using ONPA.Membership.DomainEvents;

namespace ONPA.Membership.Api.Application.DomainEventHandlers;

public sealed class MemberExpelAppealDismissedDomainEventHandler : DomainEventHandlerBase<MemberExpelAppealDismissedDomainEvent>
{
    private readonly IAggregateRepository<MembersGroup> _groupAggregateRepository;
    private readonly IIntegrationEventService _integrationEventService;

    public MemberExpelAppealDismissedDomainEventHandler(
        IAggregateRepository<MembersGroup> groupAggregateRepository, 
        IIntegrationEventService integrationEventService)
    {
        this._groupAggregateRepository = groupAggregateRepository;
        this._integrationEventService = integrationEventService;
    }

    public override async Task Handle(MemberExpelAppealDismissedDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (var group in notification.GroupMemberships)
        {
            var groupAggregate = await this._groupAggregateRepository.GetByIdAsync(group, cancellationToken);
            if (groupAggregate is null)
            {
                continue;
            }
            groupAggregate.RemoveFromGroup(notification.MemberNumber,notification.RejectDate);
            await this._groupAggregateRepository.UpdateAsync(groupAggregate, cancellationToken);
        }
    }
}