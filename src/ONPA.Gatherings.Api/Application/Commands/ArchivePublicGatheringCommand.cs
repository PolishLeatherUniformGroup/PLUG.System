using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ArchivePublicGatheringCommand(Guid TenantId,Guid PublicGatheringId) : ApplicationCommandBase(TenantId)
{
    
}