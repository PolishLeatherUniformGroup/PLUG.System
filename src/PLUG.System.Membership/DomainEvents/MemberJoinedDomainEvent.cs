using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.DomainEvents;

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

    private MemberJoinedDomainEvent(Guid aggregateId, CardNumber cardNumber, string firstName,
        string lastName,
        string email,
        string phone) : base(aggregateId)
    {
        this.CardNumber = cardNumber;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Phone = phone;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberJoinedDomainEvent(aggregateId, this.CardNumber, this.FirstName, this.LastName,this.Email, this.Phone);
    }
}