using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.DomainEvents;

public sealed class MemberLeftDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime LeaveDate { get; private set; }

    public MemberLeftDomainEvent(string firstName, string email, DateTime leaveDate)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.LeaveDate = leaveDate;
    }

    private MemberLeftDomainEvent(Guid aggregateId, string firstName, string email, DateTime leaveDate) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.LeaveDate = leaveDate;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberLeftDomainEvent(aggregateId, this.FirstName, this.Email, this.LeaveDate);
    }
}