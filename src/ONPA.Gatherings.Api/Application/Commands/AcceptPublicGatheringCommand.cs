using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record AcceptPublicGatheringCommand(Guid TenantId,Guid PublicGatheringId) : ApplicationCommandBase(TenantId)
{
 
}