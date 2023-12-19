using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ModifyEventPriceCommand(
    Guid TenantId,
    Guid EventId,
    Money PricePerPerson,
    string? Operator = null) : MultiTenantApplicationCommandBase(TenantId,
    Operator);