using ONPA.Common.Domain;

namespace ONPA.Gatherings.StateEvents;

public sealed class PublicGatheringScheduleChanged :StateEventBase
{
    public DateTime ScheduledStart { get; private set; }
    public DateTime PublishDate { get; private set; }
    public DateTime EnrollmentDeadline { get; private set; }

    public PublicGatheringScheduleChanged(DateTime scheduledStart, DateTime publishDate, DateTime enrollmentDeadline)
    {
        this.ScheduledStart = scheduledStart;
        this.PublishDate = publishDate;
        this.EnrollmentDeadline = enrollmentDeadline;
    }

    private PublicGatheringScheduleChanged(Guid aggregateId, long aggregateVersion, DateTime scheduledStart,
        DateTime publishDate,
        DateTime enrollmentDeadline) : base(aggregateId, aggregateVersion)
    {
        this.ScheduledStart = scheduledStart;
        this.PublishDate = publishDate;
        this.EnrollmentDeadline = enrollmentDeadline;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new PublicGatheringScheduleChanged(aggregateId, aggregateVersion, this.ScheduledStart, this.PublishDate, this.EnrollmentDeadline);
    }
}