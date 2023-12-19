using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ModifyEventScheduleCommand(
    Guid TenantId,
    Guid EventId,
    DateTime ScheduledStart,
    DateTime PublishDate,
    DateTime EnrollmentDeadline,
    string? Operator = null) : MultiTenantApplicationCommandBase(TenantId,
    Operator);