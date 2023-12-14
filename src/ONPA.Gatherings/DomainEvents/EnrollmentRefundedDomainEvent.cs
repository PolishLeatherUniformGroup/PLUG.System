using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.DomainEvents;

public sealed class EnrollmentRefundedDomainEvent : DomainEventBase
{
    public DateTime RefundDate { get; private set; }
    public Money RefundAmount { get; private set; }
    public string EnrollmentFirstName { get; private set; }
    public string EnrollmentEmail { get; private set; }

    public EnrollmentRefundedDomainEvent(DateTime refundDate, Money refundAmount, string enrollmentFirstName, string enrollmentEmail)
    {
        this.RefundDate = refundDate;
        this.RefundAmount = refundAmount;
        this.EnrollmentFirstName = enrollmentFirstName;
        this.EnrollmentEmail = enrollmentEmail;
    }
        
    private EnrollmentRefundedDomainEvent(Guid aggregateId, Guid tenantId, DateTime refundDate, Money refundAmount, string enrollmentFirstName, string enrollmentEmail) : base(aggregateId,tenantId)
    {
        this.RefundDate = refundDate;
        this.RefundAmount = refundAmount;
        this.EnrollmentFirstName = enrollmentFirstName;
        this.EnrollmentEmail = enrollmentEmail;
    }
        
    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new EnrollmentRefundedDomainEvent(aggregateId,tenantId, this.RefundDate, this.RefundAmount, this.EnrollmentFirstName, this.EnrollmentEmail);
    }
}