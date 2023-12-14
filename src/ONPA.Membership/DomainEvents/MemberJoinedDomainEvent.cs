using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

public sealed class MemberJoinedDomainEvent :DomainEventBase
{
    public CardNumber CardNumber { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }

    public MemberJoinedDomainEvent(CardNumber cardNumber, string firstName, string lastName,
        string email,
        string phone)
    {
        this.CardNumber = cardNumber;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Phone = phone;
    }

    private MemberJoinedDomainEvent(Guid aggregateId,Guid tenantId, CardNumber cardNumber, string firstName,
        string lastName,
        string email,
        string phone) : base(aggregateId,tenantId)
    {
        this.CardNumber = cardNumber;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Phone = phone;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new MemberJoinedDomainEvent(aggregateId,tenantId, this.CardNumber, this.FirstName, this.LastName,this.Email, this.Phone);
    }
}