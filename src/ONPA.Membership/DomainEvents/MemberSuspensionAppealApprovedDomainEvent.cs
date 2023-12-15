using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

public sealed class MemberSuspensionAppealApprovedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime ApproveDate { get; private set; }
    public string Justification { get; set; }

    public MemberSuspensionAppealApprovedDomainEvent(string firstName, string email, DateTime approveDate, string justification)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ApproveDate = approveDate;
        this.Justification = justification;
    }

    private MemberSuspensionAppealApprovedDomainEvent(Guid aggregateId,Guid tenantId, string firstName, string email, DateTime approveDate, string justification) : base(aggregateId,tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.ApproveDate = approveDate;
        this.Justification = justification;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new MemberSuspensionAppealApprovedDomainEvent(aggregateId,tenantId, this.FirstName, this.Email, this.ApproveDate, this.Justification);
    }
}