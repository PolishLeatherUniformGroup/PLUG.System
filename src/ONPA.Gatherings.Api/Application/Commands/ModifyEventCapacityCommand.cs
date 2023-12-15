using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ModifyEventCapacityCommand(Guid TenantId, Guid EventId, int? PlannedCapacity) : ApplicationCommandBase(TenantId);