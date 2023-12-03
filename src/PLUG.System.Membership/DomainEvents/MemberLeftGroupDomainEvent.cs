using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.DomainEvents;

public sealed class MemberLeftGroupDomainEvent : DomainEventBase
{
    public Guid MemberId { get; private set; }

    public MemberLeftGroupDomainEvent(Guid memberId)
    {
        this.MemberId = memberId;
    }

    private MemberLeftGroupDomainEvent(Guid aggregateId, Guid memberId) : base(aggregateId)
    {
        this.MemberId = memberId;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberLeftGroupDomainEvent(aggregateId, this.MemberId);
    }
}