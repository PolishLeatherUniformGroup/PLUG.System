using PLUG.System.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Gatherings.DomainEvents;

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
        
    private EnrollmentRefundedDomainEvent(Guid aggregateId, DateTime refundDate, Money refundAmount, string enrollmentFirstName, string enrollmentEmail) : base(aggregateId)
    {
        this.RefundDate = refundDate;
        this.RefundAmount = refundAmount;
        this.EnrollmentFirstName = enrollmentFirstName;
        this.EnrollmentEmail = enrollmentEmail;
    }
        
    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new EnrollmentRefundedDomainEvent(aggregateId, this.RefundDate, this.RefundAmount, this.EnrollmentFirstName, this.EnrollmentEmail);
    }
}