using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

public sealed class MembershipExtendedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime ValidUntil { get; private set; }

    public MembershipExtendedDomainEvent(string firstName, string email, DateTime validUntil)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ValidUntil = validUntil;
    }

    private MembershipExtendedDomainEvent(Guid aggregateId,Guid tenantId, string firstName, string email, DateTime validUntil) : base(aggregateId,tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ValidUntil = validUntil;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new MembershipExtendedDomainEvent(aggregateId,tenantId, this.FirstName, this.Email, this.ValidUntil);
    }
}