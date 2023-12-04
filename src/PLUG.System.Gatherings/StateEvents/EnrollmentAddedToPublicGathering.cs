using PLUG.System.Common.Domain;
using PLUG.System.Gatherings.Domain;

namespace PLUG.System.Gatherings.StateEvents;

public sealed class EnrollmentAddedToPublicGathering : StateEventBase
{
    public PublicGatheringEnrollment Enrollment { get; private set; }

    public EnrollmentAddedToPublicGathering(PublicGatheringEnrollment enrollment)
    {
        this.Enrollment = enrollment;
    }

    private EnrollmentAddedToPublicGathering(Guid aggregateId, long aggregateVersion, PublicGatheringEnrollment enrollment) : base(aggregateId, aggregateVersion)
    {
        this.Enrollment = enrollment;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new EnrollmentAddedToPublicGathering(aggregateId, aggregateVersion, this.Enrollment);
    }
}