using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.DomainEvents;

public sealed class MemberExpelledDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public string Justification { get; private set; }
    public DateTime ExpelDate { get; private set; }

    public DateTime AppealDeadline { get; private set; }

    public MemberExpelledDomainEvent(string firstName, string email, string justification,
        DateTime expelDate, DateTime appealDeadline)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.Justification = justification;
        this.ExpelDate = expelDate;
        this.AppealDeadline = appealDeadline;
    }

    private MemberExpelledDomainEvent(Guid aggregateId, string firstName, string email,
        string justification,
        DateTime expelDate,
        DateTime appealDeadline) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.Justification = justification;
        this.ExpelDate = expelDate;
        this.AppealDeadline = appealDeadline;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberExpelledDomainEvent(aggregateId,
            this.FirstName,
            this.Email,
            this.Justification,
            this.ExpelDate,
            this.AppealDeadline);
    }
}