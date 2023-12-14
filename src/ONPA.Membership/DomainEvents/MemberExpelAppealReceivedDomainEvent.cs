using ONPA.Common.Domain;

namespace ONPA.Membership.DomainEvents;

public sealed class MemberExpelAppealReceivedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime AppealDate { get; private set; }

    public MemberExpelAppealReceivedDomainEvent(string firstName, string email, DateTime appealDate)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.AppealDate = appealDate;
    }

    private MemberExpelAppealReceivedDomainEvent(Guid aggregateId,Guid tenantId, string firstName, string email,
        DateTime appealDate) : base(aggregateId,tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.AppealDate = appealDate;

    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new MemberExpelAppealReceivedDomainEvent(aggregateId,tenantId, this.FirstName,this.Email,this.AppealDate);
    }
}