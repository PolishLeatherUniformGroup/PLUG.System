using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

public sealed class MemberLeftGroupDomainEvent : DomainEventBase
{
    public Guid MemberId { get; private set; }

    public MemberLeftGroupDomainEvent(Guid memberId)
    {
        this.MemberId = memberId;
    }

    private MemberLeftGroupDomainEvent(Guid aggregateId,Guid tenantId, Guid memberId) : base(aggregateId,tenantId)
    {
        this.MemberId = memberId;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new MemberLeftGroupDomainEvent(aggregateId,tenantId, this.MemberId);
    }
}