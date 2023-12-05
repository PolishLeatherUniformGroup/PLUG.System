using ONPA.Common.Domain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationRejectedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime RejectDate { get; private set; }
    public string DecisionDetail { get; private set; }
    public DateTime AppealDeadline { get; private set; }

    public ApplicationRejectedDomainEvent(string firstName, string email, DateTime rejectDate,
        string decisionDetail,
        DateTime appealDeadline)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RejectDate = rejectDate;
        this.DecisionDetail = decisionDetail;
        this.AppealDeadline = appealDeadline;
    }

    public ApplicationRejectedDomainEvent(Guid aggregateId, string firstName, string email,
        DateTime rejectDate,
        string decisionDetail,
        DateTime appealDeadline) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RejectDate = rejectDate;
        this.DecisionDetail = decisionDetail;
        this.AppealDeadline = appealDeadline;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new ApplicationRejectedDomainEvent(aggregateId,this.FirstName,this.Email, this.RejectDate, this.DecisionDetail,
            this.AppealDeadline);
    }
}