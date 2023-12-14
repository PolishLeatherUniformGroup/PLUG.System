using MediatR;

namespace ONPA.Common.Application;

public abstract record ApplicationCommandBase(Guid TenantId) : IApplicationCommand, IRequest<CommandResult>
{
    
}