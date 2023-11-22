using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.DomainEvents;

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

    private MembershipExtendedDomainEvent(Guid aggregateId, string firstName, string email, DateTime validUntil) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ValidUntil = validUntil;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MembershipExtendedDomainEvent(aggregateId, this.FirstName, this.Email, this.ValidUntil);
    }
}