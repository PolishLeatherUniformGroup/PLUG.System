using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

public sealed class MembershipExpiredDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public string Reason { get; private set; }

    public MembershipExpiredDomainEvent(string firstName, string email, DateTime expirationDate, string reason)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ExpirationDate = expirationDate;
        this.Reason = reason;
    }

    private MembershipExpiredDomainEvent(Guid aggregateId, string firstName, string email, DateTime expirationDate, string reason) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ExpirationDate = expirationDate;
        this.Reason = reason;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MembershipExpiredDomainEvent(aggregateId, this.FirstName, this.Email, this.ExpirationDate,this.Reason);
    }
}