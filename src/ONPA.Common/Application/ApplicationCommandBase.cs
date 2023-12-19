using MediatR;

namespace ONPA.Common.Application;

public abstract record MultiTenantApplicationCommandBase(Guid TenantId, string? Operator = null)
    : IApplicationCommand, IRequest<CommandResult>;
    
public abstract record ApplicationCommandBase(string? Operator=null) : IApplicationCommand, IRequest<CommandResult>;