using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.DomainEvents;

public sealed class MemberExpelAppealDismissedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime RejectDate { get; private set; }
    public string DecisionDetails { get; set; }

    public MemberExpelAppealDismissedDomainEvent(string firstName,
        string email,
        DateTime rejectDate,
        string decisionDetails)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RejectDate = rejectDate;
        this.DecisionDetails = decisionDetails;
    }

    private MemberExpelAppealDismissedDomainEvent(Guid aggregateId,
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
        return new MemberExpelAppealDismissedDomainEvent(aggregateId, this.FirstName, this.Email, this.RejectDate, this.DecisionDetails);
    }
}