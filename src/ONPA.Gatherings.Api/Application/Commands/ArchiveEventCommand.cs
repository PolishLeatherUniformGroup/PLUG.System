using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ArchiveEventCommand(Guid TenantId,Guid EventId) : ApplicationCommandBase(TenantId)
{
    
}