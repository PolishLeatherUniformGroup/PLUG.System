using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record AcceptEventCommand(
    Guid TenantId,
    Guid EventId,
    string? Operator = null) : MultiTenantApplicationCommandBase(TenantId,
    Operator)
{
 
}