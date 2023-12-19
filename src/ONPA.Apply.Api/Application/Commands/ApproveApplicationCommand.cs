using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record ApproveApplicationCommand(
    Guid TenantId,
    Guid ApplicationId,
    DateTime DecisionDate,
    string? Operator = null) : MultiTenantApplicationCommandBase(TenantId,
    Operator);