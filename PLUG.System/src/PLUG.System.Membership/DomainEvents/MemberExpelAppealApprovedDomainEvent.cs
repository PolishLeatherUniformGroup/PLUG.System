using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.DomainEvents;

public sealed class MemberExpelAppealApprovedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime ApproveDate { get; private set; }
    public string Justification { get; set; }

    public MemberExpelAppealApprovedDomainEvent(string firstName, string email, DateTime approveDate, string justification)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ApproveDate = approveDate;
        this.Justification = justification;
    }

    private MemberExpelAppealApprovedDomainEvent(Guid aggregateId, string firstName, string email, DateTime approveDate, string justification) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ApproveDate = approveDate;
        this.Justification = justification;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberExpelAppealApprovedDomainEvent(aggregateId, this.FirstName, this.Email, this.ApproveDate, this.Justification);
    }
}