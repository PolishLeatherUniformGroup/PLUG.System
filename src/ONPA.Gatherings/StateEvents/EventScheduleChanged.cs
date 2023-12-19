﻿using ONPA.Common.Domain;

namespace ONPA.Gatherings.StateEvents;

public sealed class EventScheduleChanged :StateEventBase
{
    public DateTime ScheduledStart { get; private set; }
    public DateTime PublishDate { get; private set; }
    public DateTime EnrollmentDeadline { get; private set; }

    public EventScheduleChanged(DateTime scheduledStart, DateTime publishDate, DateTime enrollmentDeadline)
    {
        this.ScheduledStart = scheduledStart;
        this.PublishDate = publishDate;
        this.EnrollmentDeadline = enrollmentDeadline;
    }

    private EventScheduleChanged(Guid tenantId, Guid aggregateId, long aggregateVersion, DateTime scheduledStart,
        DateTime publishDate,
        DateTime enrollmentDeadline) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.ScheduledStart = scheduledStart;
        this.PublishDate = publishDate;
        this.EnrollmentDeadline = enrollmentDeadline;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new EventScheduleChanged(tenantId, aggregateId, aggregateVersion, this.ScheduledStart, this.PublishDate, this.EnrollmentDeadline);
    }
}