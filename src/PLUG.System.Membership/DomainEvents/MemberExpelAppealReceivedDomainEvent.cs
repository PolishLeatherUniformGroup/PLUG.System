using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.DomainEvents;

public sealed class MemberExpelAppealReceivedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime AppealDate { get; private set; }

    public MemberExpelAppealReceivedDomainEvent(string firstName, string email, DateTime appealDate)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.AppealDate = appealDate;
    }

    private MemberExpelAppealReceivedDomainEvent(Guid aggregateId, string firstName, string email,
        DateTime appealDate) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.AppealDate = appealDate;

    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberExpelAppealReceivedDomainEvent(aggregateId, this.FirstName,this.Email,this.AppealDate);
    }
}