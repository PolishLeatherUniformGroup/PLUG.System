using MediatR;

namespace ONPA.Common.Application;

public abstract class ApplicationCommandHandlerBase<TCommand> : IRequestHandler<TCommand,CommandResult>
    where TCommand : ApplicationCommandBase
{
    public abstract Task<CommandResult> Handle(TCommand request, CancellationToken cancellationToken);
}