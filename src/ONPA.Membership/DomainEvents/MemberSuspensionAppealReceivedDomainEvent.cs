using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

public sealed class MemberSuspensionAppealReceivedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime AppealDate { get; private set; }

    public MemberSuspensionAppealReceivedDomainEvent(string firstName, string email, DateTime appealDate)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.AppealDate = appealDate;
    }

    private MemberSuspensionAppealReceivedDomainEvent(Guid aggregateId, string firstName, string email,
        DateTime appealDate) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.AppealDate = appealDate;

    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberSuspensionAppealReceivedDomainEvent(aggregateId, this.FirstName,this.Email,this.AppealDate);
    }
}