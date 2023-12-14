using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ModifyPublicGatheringCapacityCommand(Guid TenantId, Guid PublicGatheringId, int? PlannedCapacity) : ApplicationCommandBase(TenantId);