using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ModifyEventDescriptionCommand(Guid TenantId, Guid PublicGatheringId, string Description, string Name, string Regulations):ApplicationCommandBase(TenantId);