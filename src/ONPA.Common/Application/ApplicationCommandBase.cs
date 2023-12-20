using MediatR;

namespace ONPA.Common.Application;

public abstract record ApplicationCommandBase(string? Operator=null) : IApplicationCommand, IRequest<CommandResult>;