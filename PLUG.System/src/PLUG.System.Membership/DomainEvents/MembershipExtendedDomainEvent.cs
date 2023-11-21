using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.DomainEvents;

public sealed class MembershipExtendedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime ValidUntil { get; private set; }

    public MembershipExtendedDomainEvent(string firstName, string email, DateTime validUntil)
    {
        FirstName = firstName;
        Email = email;
        ValidUntil = validUntil;
    }

    private MembershipExtendedDomainEvent(Guid aggregateId, string firstName, string email, DateTime validUntil) : base(aggregateId)
    {
        FirstName = firstName;
        Email = email;
        ValidUntil = validUntil;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MembershipExtendedDomainEvent(aggregateId, FirstName, Email, ValidUntil);
    }
}