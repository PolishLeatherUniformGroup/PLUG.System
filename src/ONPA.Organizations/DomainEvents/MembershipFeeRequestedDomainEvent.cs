using ONPA.Common.Domain;
using ONPA.Organizations.Domain;

namespace ONPA.Organizations.DomainEvents;

public sealed class MembershipFeeRequestedDomainEvent :DomainEventBase
{
    public MembershipFee Fee { get; private set; }
    public DateTime PaymentDeadline { get; }

    public MembershipFeeRequestedDomainEvent(MembershipFee fee, DateTime paymentDeadline)
    {
        this.Fee = fee;
        this.PaymentDeadline = paymentDeadline;
    }
    
    private MembershipFeeRequestedDomainEvent(Guid aggregateId, MembershipFee fee,DateTime paymentDeadline) : base(aggregateId)
    {
        this.Fee = fee;
        this.PaymentDeadline = paymentDeadline;
    } 
    
    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MembershipFeeRequestedDomainEvent(aggregateId, this.Fee, this.PaymentDeadline);
    }
}