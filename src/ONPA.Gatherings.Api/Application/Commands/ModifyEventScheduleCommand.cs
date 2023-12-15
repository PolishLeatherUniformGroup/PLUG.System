using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ModifyEventScheduleCommand(Guid TenantId, Guid PublicGatheringId, DateTime ScheduledStart, DateTime PublishDate, DateTime EnrollmentDeadline) : ApplicationCommandBase(TenantId);