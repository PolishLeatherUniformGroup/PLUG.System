using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.DomainEvents;

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

    private EnrollmentPaymentRegisteredDomainEvent(Guid aggregateId, Guid tenantId, DateTime paidDate, Money paidAmount, string enrollmentFirstName, string enrollmentEmail) : base(aggregateId,tenantId)
    {
        this.PaidDate = paidDate;
        this.PaidAmount = paidAmount;
        this.EnrollmentFirstName = enrollmentFirstName;
        this.EnrollmentEmail = enrollmentEmail;
    }
        
    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new EnrollmentPaymentRegisteredDomainEvent(aggregateId,tenantId, this.PaidDate, this.PaidAmount, this.EnrollmentFirstName, this.EnrollmentEmail);
    }
}