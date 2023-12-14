using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

public sealed class MemberJoinedGroupDomainEvent : DomainEventBase
{
    public Guid MemberId { get; private set; }

    public MemberJoinedGroupDomainEvent(Guid memberId)
    {
        this.MemberId = memberId;
    }

    private MemberJoinedGroupDomainEvent(Guid aggregateId,Guid tenantId, Guid memberId) : base(aggregateId,tenantId)
    {
        this.MemberId = memberId;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new MemberJoinedGroupDomainEvent(aggregateId,tenantId, this.MemberId);
    }
}