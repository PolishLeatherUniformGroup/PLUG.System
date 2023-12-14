using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Membership.DomainEvents;

public sealed class MemberFeePaymentRegisteredDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public Money RegisteredFee { get; private set; }
    public Money RequiredFee{ get; private set; }
    public DateTime PaidDate { get; private set; }
    public DateTime Period { get;  private set; }    

    public MemberFeePaymentRegisteredDomainEvent(string firstName, string email, Money registeredFee, Money requiredFee, DateTime paidDate, DateTime period)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RegisteredFee = registeredFee;
        this.RequiredFee = requiredFee;
        this.PaidDate = paidDate;
        this.Period = period;
    }

    private MemberFeePaymentRegisteredDomainEvent(Guid aggregateId,Guid tenantId, string firstName, string email, Money registeredFee, Money requiredFee, DateTime paidDate, DateTime period) : base(aggregateId,tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RegisteredFee = registeredFee;
        this.RequiredFee = requiredFee;
        this.PaidDate = paidDate;
        this.Period = period;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new MemberFeePaymentRegisteredDomainEvent(aggregateId,tenantId, this.FirstName, this.Email, this.RegisteredFee, this.RequiredFee, this.PaidDate, this.Period);
    }
}