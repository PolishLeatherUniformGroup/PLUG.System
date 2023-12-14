using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

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

    private MemberExpelledDomainEvent(Guid aggregateId,Guid tenantId, string firstName, string email,
        string justification,
        DateTime expelDate,
        DateTime appealDeadline) : base(aggregateId,tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.Justification = justification;
        this.ExpelDate = expelDate;
        this.AppealDeadline = appealDeadline;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new MemberExpelledDomainEvent(aggregateId,tenantId,
            this.FirstName,
            this.Email,
            this.Justification,
            this.ExpelDate,
            this.AppealDeadline);
    }
}