using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Membership.Domain;
using ONPA.Membership.DomainEvents;

namespace ONPA.Membership.Api.Application.DomainEventHandlers;

public sealed class MemberLeftGroupDomainEventHandler : DomainEventHandlerBase<MemberLeftGroupDomainEvent>
{
    private readonly IMultiTenantAggregateRepository<Member> _memberAggregateRepository;

    public MemberLeftGroupDomainEventHandler(IMultiTenantAggregateRepository<Member> memberAggregateRepository)
    {
        this._memberAggregateRepository = memberAggregateRepository;
    }

    public override async Task Handle(MemberLeftGroupDomainEvent notification, CancellationToken cancellationToken)
    {
        var memberAggregate = await this._memberAggregateRepository.GetByIdAsync(notification.TenantId, notification.MemberId, cancellationToken);
        if (memberAggregate is null)
        {
            throw new AggregateNotFoundException();
        }
        memberAggregate.RemoveGroupMembership(notification.AggregateId);
        await this._memberAggregateRepository.UpdateAsync(memberAggregate,cancellationToken);
    }
}