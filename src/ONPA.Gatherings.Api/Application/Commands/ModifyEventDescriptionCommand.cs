using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ModifyEventDescriptionCommand(
    Guid TenantId,
    Guid EventId,
    string Description,
    string Name,
    string Regulations,
    string? Operator = null) : MultiTenantApplicationCommandBase(TenantId,
    Operator);