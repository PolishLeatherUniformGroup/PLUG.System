using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Membership.Domain;
using PLUG.System.Membership.DomainEvents;

namespace PLUG.System.Membership.Api.Application.DomainEventHandlers;

public sealed class MemberJoinedGroupDomainEventHandler : DomainEventHandlerBase<MemberJoinedGroupDomainEvent>
{
    private readonly IAggregateRepository<Member> _memberAggregateRepository;

    public MemberJoinedGroupDomainEventHandler(IAggregateRepository<Member> memberAggregateRepository)
    {
        this._memberAggregateRepository = memberAggregateRepository;
    }

    public override async Task Handle(MemberJoinedGroupDomainEvent notification, CancellationToken cancellationToken)
    {
        var memberAggregate = await this._memberAggregateRepository.GetByIdAsync(notification.MemberId, cancellationToken);
        if (memberAggregate is null)
        {
            throw new AggregateNotFoundException();
        }
        memberAggregate.AddGroupMembership(notification.AggregateId);
        await this._memberAggregateRepository.UpdateAsync(memberAggregate,cancellationToken);
    }
}