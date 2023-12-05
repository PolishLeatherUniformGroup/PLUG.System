using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.StateEvents;

public class EnrollmentRefunded : StateEventBase
{
    public Guid EnrollmentId { get; private set; }
    public DateTime RefundDate { get; private set; }
    public Money RefundAmount { get; private set; }

    public EnrollmentRefunded(Guid enrollmentId, DateTime refundDate, Money refundAmount)
    {
        this.EnrollmentId = enrollmentId;
        this.RefundDate = refundDate;
        this.RefundAmount = refundAmount;
    }
        
    private EnrollmentRefunded(Guid aggregateId, long aggregateVersion, Guid enrollmentId, DateTime refundDate, Money refundAmount) : base(aggregateId, aggregateVersion)
    {
        this.EnrollmentId = enrollmentId;
        this.RefundDate = refundDate;
        this.RefundAmount = refundAmount;
    }
        
    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new EnrollmentRefunded(aggregateId, aggregateVersion, this.EnrollmentId, this.RefundDate, this.RefundAmount);
    }
}