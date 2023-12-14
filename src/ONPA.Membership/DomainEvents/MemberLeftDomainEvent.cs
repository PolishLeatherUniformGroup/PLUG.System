using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

public sealed class MemberLeftDomainEvent : DomainEventBase
{
    public CardNumber CardNumber { get; private set; }
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime LeaveDate { get; private set; }
    public List<Guid> Groups { get; private set; }

    public MemberLeftDomainEvent( CardNumber cardNumber, string firstName, string email, DateTime leaveDate,
        List<Guid> groups)
    {
        this.CardNumber = cardNumber;
        this.FirstName = firstName;
        this.Email = email;
        this.LeaveDate = leaveDate;
        this.Groups = groups;
    }

    private MemberLeftDomainEvent(Guid aggregateId,Guid tenantId, CardNumber cardNumber, string firstName, string email, DateTime leaveDate,
        List<Guid> groups) : base(aggregateId,tenantId)
    {
        this.CardNumber = cardNumber;
        this.FirstName = firstName;
        this.Email = email;
        this.LeaveDate = leaveDate;
        this.Groups = groups;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new MemberLeftDomainEvent(aggregateId,tenantId, this.CardNumber, this.FirstName, this.Email, this.LeaveDate, this.Groups);
    }
}