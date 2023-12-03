using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.DomainEvents;

public sealed class MemberExpelAppealDismissedDomainEvent : DomainEventBase
{
    public CardNumber MemberNumber { get; private set; }
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime RejectDate { get; private set; }
    public string DecisionDetails { get; set; }
    public List<Guid> GroupMemberships { get; private set; }

    public MemberExpelAppealDismissedDomainEvent(CardNumber memberNumber,
        string firstName,
        string email,
        DateTime rejectDate,
        string decisionDetails,
        List<Guid> groupMemberships)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RejectDate = rejectDate;
        this.DecisionDetails = decisionDetails;
    }

    private MemberExpelAppealDismissedDomainEvent(Guid aggregateId,
        CardNumber memberNumber,
        string firstName,
        string email,
        DateTime rejectDate,
        string decisionDetails,
        List<Guid> groupMemberships) : base(aggregateId)
    {
        this.MemberNumber = memberNumber;
        this.FirstName = firstName;
        this.Email = email;
        this.RejectDate = rejectDate;
        this.DecisionDetails = decisionDetails;
        this.GroupMemberships = groupMemberships;
    }


    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberExpelAppealDismissedDomainEvent(aggregateId, this.MemberNumber,this.FirstName, this.Email, this.RejectDate, this.DecisionDetails,this.GroupMemberships);
    }
}