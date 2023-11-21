using MediatR;

namespace PLUG.System.Common.Application;

public abstract record ApplicationCommandBase : IApplicationCommand, IRequest<CommandResult>
{
    
}