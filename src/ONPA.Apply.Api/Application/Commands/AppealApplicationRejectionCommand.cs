using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record AppealApplicationRejectionCommand(
    Guid TenantId,
    Guid ApplicationId,
    string Justification,
    DateTime AppealReceived,
    string? Operator = null) : MultiTenantApplicationCommandBase(TenantId,
    Operator);
