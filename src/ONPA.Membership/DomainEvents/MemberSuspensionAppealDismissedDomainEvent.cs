using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

public sealed class MemberSuspensionAppealDismissedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime RejectDate { get; private set; }
    public string DecisionDetails { get; set; }

    public MemberSuspensionAppealDismissedDomainEvent(string firstName,
        string email,
        DateTime rejectDate,
        string decisionDetails)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RejectDate = rejectDate;
        this.DecisionDetails = decisionDetails;
    }

    private MemberSuspensionAppealDismissedDomainEvent(Guid aggregateId,Guid tenantId,
        string firstName,
        string email,
        DateTime rejectDate,
        string decisionDetails) : base(aggregateId,tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RejectDate = rejectDate;
        this.DecisionDetails = decisionDetails;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new MemberSuspensionAppealDismissedDomainEvent(aggregateId,tenantId, this.FirstName, this.Email, this.RejectDate, this.DecisionDetails);
    }
}