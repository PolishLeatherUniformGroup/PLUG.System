using PLUG.System.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Membership.DomainEvents;

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
        FirstName = firstName;
        Email = email;
        RegisteredFee = registeredFee;
        RequiredFee = requiredFee;
        PaidDate = paidDate;
        Period = period;
    }

    private MemberFeePaymentRegisteredDomainEvent(Guid aggregateId, string firstName, string email, Money registeredFee, Money requiredFee, DateTime paidDate, DateTime period) : base(aggregateId)
    {
        FirstName = firstName;
        Email = email;
        RegisteredFee = registeredFee;
        RequiredFee = requiredFee;
        PaidDate = paidDate;
        Period = period;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new MemberFeePaymentRegisteredDomainEvent(aggregateId, FirstName, Email, RegisteredFee,RequiredFee, PaidDate, Period);
    }
}