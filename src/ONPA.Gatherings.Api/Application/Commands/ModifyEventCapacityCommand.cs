using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ModifyEventCapacityCommand(Guid TenantId, Guid PublicGatheringId, int? PlannedCapacity) : ApplicationCommandBase(TenantId);