using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record AcceptEventCommand(Guid TenantId,Guid EventId) : ApplicationCommandBase(TenantId)
{
 
}