using PLUG.System.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Gatherings.DomainEvents;

public sealed class EnrollmentPaymentRegisteredDomainEvent : DomainEventBase
{
    public DateTime PaidDate { get; private set; }
    public Money PaidAmount { get; private set; }
    public string EnrollmentFirstName { get; private set; }
    public string EnrollmentEmail { get; private set; }

    public EnrollmentPaymentRegisteredDomainEvent(DateTime paidDate, Money paidAmount, string enrollmentFirstName, string enrollmentEmail)
    {
        this.PaidDate = paidDate;
        this.PaidAmount = paidAmount;
        this.EnrollmentFirstName = enrollmentFirstName;
        this.EnrollmentEmail = enrollmentEmail;
    }

    private EnrollmentPaymentRegisteredDomainEvent(Guid aggregateId, DateTime paidDate, Money paidAmount, string enrollmentFirstName, string enrollmentEmail) : base(aggregateId)
    {
        this.PaidDate = paidDate;
        this.PaidAmount = paidAmount;
        this.EnrollmentFirstName = enrollmentFirstName;
        this.EnrollmentEmail = enrollmentEmail;
    }
        
    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new EnrollmentPaymentRegisteredDomainEvent(aggregateId, this.PaidDate, this.PaidAmount, this.EnrollmentFirstName, this.EnrollmentEmail);
    }
}