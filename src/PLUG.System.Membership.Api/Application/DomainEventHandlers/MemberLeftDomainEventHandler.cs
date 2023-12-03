using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Membership.Api.Application.IntegrationEvents;
using PLUG.System.Membership.Domain;
using PLUG.System.Membership.DomainEvents;

namespace PLUG.System.Membership.Api.Application.DomainEventHandlers;

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