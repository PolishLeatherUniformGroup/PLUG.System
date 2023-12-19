using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record CancelEnrollmentCommand(
    Guid TenantId,
    Guid EventId,
    Guid EnrollmentId,
    Money RefundableAmount,
    DateTime CancellationDate,
    string? Operator = null) : MultiTenantApplicationCommandBase(TenantId,
    Operator);