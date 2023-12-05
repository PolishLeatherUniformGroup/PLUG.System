using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ModifyPublicGatheringScheduleCommand : ApplicationCommandBase
{
    public Guid PublicGatheringId { get; init; }
    public DateTime ScheduledStart { get; init; }
    public DateTime PublishDate { get; init; }
    public DateTime EnrollmentDeadline { get; init; }
    
    public ModifyPublicGatheringScheduleCommand(Guid publicGatheringId, DateTime scheduledStart, DateTime publishDate, DateTime enrollmentDeadline)
    {
        this.PublicGatheringId = publicGatheringId;
        this.ScheduledStart = scheduledStart;
        this.PublishDate = publishDate;
        this.EnrollmentDeadline = enrollmentDeadline;
    }
}