using ONPA.Common.Domain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationRejectionAppealDismissedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime RejectDate { get; private set; }
    public string DecisionDetails { get; set; }

    public ApplicationRejectionAppealDismissedDomainEvent(string firstName,
        string email,
        DateTime rejectDate,
        string decisionDetails)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RejectDate = rejectDate;
        this.DecisionDetails = decisionDetails;
    }

    private ApplicationRejectionAppealDismissedDomainEvent(Guid aggregateId,
        string firstName,
        string email,
        DateTime rejectDate,
        string decisionDetails) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RejectDate = rejectDate;
        this.DecisionDetails = decisionDetails;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new ApplicationRejectionAppealDismissedDomainEvent(aggregateId, this.FirstName, this.Email, this.RejectDate, this.DecisionDetails);
    }
}