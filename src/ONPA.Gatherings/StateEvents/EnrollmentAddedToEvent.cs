using ONPA.Gatherings.Domain;
using ONPA.Common.Domain;

namespace ONPA.Gatherings.StateEvents;

public sealed class EnrollmentAddedToEvent : StateEventBase
{
    public EventEnrollment Enrollment { get; private set; }

    public EnrollmentAddedToEvent(EventEnrollment enrollment)
    {
        this.Enrollment = enrollment;
    }

    private EnrollmentAddedToEvent(Guid aggregateId, long aggregateVersion, EventEnrollment enrollment) : base(aggregateId, aggregateVersion)
    {
        this.Enrollment = enrollment;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new EnrollmentAddedToEvent(aggregateId, aggregateVersion, this.Enrollment);
    }
}