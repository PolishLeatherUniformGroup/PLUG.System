using MediatR;

namespace ONPA.Common.Application;

public abstract record ApplicationCommandBase : IApplicationCommand, IRequest<CommandResult>
{
    
}