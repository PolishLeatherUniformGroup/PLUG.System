using ONPA.Gatherings.Domain;
using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.DomainEvents;

public sealed class EnrollmentCancelledDomainEvent : DomainEventBase
{
    public DateTime CancellationDate { get; private set; }
    public Money RefundableAmount { get; private set; }
    public string EnrollmentFirstName { get; private set; }
    public string EnrollmentEmail { get; private set; }
    public List<Participant> Participants { get; private set; }

    public EnrollmentCancelledDomainEvent(DateTime cancellationDate, Money refundableAmount, string enrollmentFirstName, string enrollmentEmail, List<Participant> participants)
    {
        this.CancellationDate = cancellationDate;
        this.RefundableAmount = refundableAmount;
        this.EnrollmentFirstName = enrollmentFirstName;
        this.EnrollmentEmail = enrollmentEmail;
        this.Participants = participants;
    }

    private EnrollmentCancelledDomainEvent(Guid aggregateId, Guid tenantId, DateTime cancellationDate, Money refundableAmount, string enrollmentFirstName, string enrollmentEmail, List<Participant> participants) : base(aggregateId,tenantId)
    {
        this.CancellationDate = cancellationDate;
        this.RefundableAmount = refundableAmount;
        this.EnrollmentFirstName = enrollmentFirstName;
        this.EnrollmentEmail = enrollmentEmail;
        this.Participants = participants;
    }
        
    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new EnrollmentCancelledDomainEvent(aggregateId,tenantId, this.CancellationDate, this.RefundableAmount, this.EnrollmentFirstName, this.EnrollmentEmail, this.Participants);
    }
}