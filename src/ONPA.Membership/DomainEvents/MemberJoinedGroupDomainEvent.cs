using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

public sealed class MemberJoinedGroupDomainEvent : DomainEventBase
{
    public Guid MemberId { get; private set; }

    public MemberJoinedGroupDomainEvent(Guid memberId)
    {
        this.MemberId = memberId;
    }

    private MemberJoinedGroupDomainEvent(Guid aggregateId, Guid memberId) : base(aggregateId)
    {
        this.MemberId = memberId;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberJoinedGroupDomainEvent(aggregateId, MemberId);
    }
}