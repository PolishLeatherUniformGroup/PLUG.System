using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.StateEvents;

public sealed class EnrollmentCancelled : StateEventBase
{
    public Guid EnrollmentId { get; private set; }
    public DateTime CancellationDate { get; private set; }
    public Money RefundableAmount { get; private set; }

    public EnrollmentCancelled(Guid enrollmentId, DateTime cancellationDate, Money refundableAmount)
    {
        this.EnrollmentId = enrollmentId;
        this.CancellationDate = cancellationDate;
        this.RefundableAmount = refundableAmount;
    }
        
    private EnrollmentCancelled(Guid aggregateId, long aggregateVersion, Guid enrollmentId, DateTime cancellationDate, Money refundableAmount) : base(aggregateId, aggregateVersion)
    {
        this.EnrollmentId = enrollmentId;
        this.CancellationDate = cancellationDate;
        this.RefundableAmount = refundableAmount;
    }
        
    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new EnrollmentCancelled(aggregateId, aggregateVersion, this.EnrollmentId, this.CancellationDate, this.RefundableAmount);
    }
}