using MediatR;

namespace ONPA.Common.Application;

public abstract class MultiTenantApplicationCommandHandlerBase<TCommand> : IRequestHandler<TCommand,CommandResult>
    where TCommand : MultiTenantApplicationCommandBase
{
    public abstract Task<CommandResult> Handle(TCommand request, CancellationToken cancellationToken);
}