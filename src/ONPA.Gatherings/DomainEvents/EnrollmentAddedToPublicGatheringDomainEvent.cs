using ONPA.Gatherings.Domain;
using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.DomainEvents;

public sealed class EnrollmentAddedToPublicGatheringDomainEvent : DomainEventBase
{
    public Money EnrollmentRequiredPayment { get; private set; }
    public string EnrollmentFirstName { get; private set; }
    public string EnrollmentEmail { get; private set; }
    public List<Participant> Participants { get; private set; }
    public DateTime ScheduledStart { get; private set; }

    public EnrollmentAddedToPublicGatheringDomainEvent(Money enrollmentRequiredPayment, string enrollmentFirstName, string enrollmentEmail, List<Participant> participants, DateTime scheduledStart)
    {
        this.EnrollmentRequiredPayment = enrollmentRequiredPayment;
        this.EnrollmentFirstName = enrollmentFirstName;
        this.EnrollmentEmail = enrollmentEmail;
        this.Participants = participants;
        this.ScheduledStart = scheduledStart;
    }
        
    private EnrollmentAddedToPublicGatheringDomainEvent(Guid aggregateId, Guid tenantId, Money enrollmentRequiredPayment, string enrollmentFirstName, string enrollmentEmail, List<Participant> participants, DateTime scheduledStart) : base(aggregateId,tenantId)
    {
        this.EnrollmentRequiredPayment = enrollmentRequiredPayment;
        this.EnrollmentFirstName = enrollmentFirstName;
        this.EnrollmentEmail = enrollmentEmail;
        this.Participants = participants;
        this.ScheduledStart = scheduledStart;
    }
        
    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new EnrollmentAddedToPublicGatheringDomainEvent(aggregateId,tenantId, this.EnrollmentRequiredPayment, this.EnrollmentFirstName, this.EnrollmentEmail, this.Participants, this.ScheduledStart);
    }
}