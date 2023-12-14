using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record CancelPublicGatheringCommand(Guid TenantId, Guid PublicGatheringId, DateTime CancellationDate) : ApplicationCommandBase(TenantId);