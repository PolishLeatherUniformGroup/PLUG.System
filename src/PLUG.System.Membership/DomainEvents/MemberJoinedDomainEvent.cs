using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.DomainEvents;

public sealed class MemberJoinedDomainEvent :DomainEventBase
{
    public CardNumber CardNumber { get; private set; }
    public string FirstName { get; private set; }
    public string Email { get; private set; }

    public MemberJoinedDomainEvent(CardNumber cardNumber, string firstName, string email)
    {
        this.CardNumber = cardNumber;
        this.FirstName = firstName;
        this.Email = email;
    }

    private MemberJoinedDomainEvent(Guid aggregateId, CardNumber cardNumber, string firstName, string email) : base(aggregateId)
    {
        this.CardNumber = cardNumber;
        this.FirstName = firstName;
        this.Email = email;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberJoinedDomainEvent(aggregateId, this.CardNumber, this.FirstName, this.Email);
    }
}