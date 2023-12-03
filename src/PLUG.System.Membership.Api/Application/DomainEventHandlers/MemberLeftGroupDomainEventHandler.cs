using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Membership.Domain;
using PLUG.System.Membership.DomainEvents;

namespace PLUG.System.Membership.Api.Application.DomainEventHandlers;

public sealed class MemberLeftGroupDomainEventHandler : DomainEventHandlerBase<MemberLeftGroupDomainEvent>
{
    private readonly IAggregateRepository<Member> _memberAggregateRepository;

    public MemberLeftGroupDomainEventHandler(IAggregateRepository<Member> memberAggregateRepository)
    {
        this._memberAggregateRepository = memberAggregateRepository;
    }

    public override async Task Handle(MemberLeftGroupDomainEvent notification, CancellationToken cancellationToken)
    {
        var memberAggregate = await this._memberAggregateRepository.GetByIdAsync(notification.MemberId, cancellationToken);
        if (memberAggregate is null)
        {
            throw new AggregateNotFoundException();
        }
        memberAggregate.RemoveGroupMembership(notification.AggregateId);
        await this._memberAggregateRepository.UpdateAsync(memberAggregate,cancellationToken);
    }
}