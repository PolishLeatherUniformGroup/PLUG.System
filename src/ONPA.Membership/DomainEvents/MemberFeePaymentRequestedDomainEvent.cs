using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Membership.DomainEvents;

public sealed class MemberFeePaymentRequestedDomainEvent :DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public Money RequestedFee { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime Period { get; private set; }

    public MemberFeePaymentRequestedDomainEvent(string firstName, string email, Money requestedFee, DateTime dueDate, DateTime period)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RequestedFee = requestedFee;
        this.DueDate = dueDate;
        this.Period = period;
    }

    private MemberFeePaymentRequestedDomainEvent(Guid aggregateId,Guid tenantId, string firstName, string email, Money requestedFee, DateTime dueDate, DateTime period) : base(aggregateId,tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RequestedFee = requestedFee;
        this.DueDate = dueDate;
        this.Period = period;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new MemberFeePaymentRequestedDomainEvent(aggregateId,tenantId, this.FirstName, this.Email, this.RequestedFee, this.DueDate, this.Period);
    }
}