using MediatR;

namespace ONPA.Common.Application;

public abstract record MultiTenantApplicationCommandBase(Guid TenantId, string? Operator = null)
    : IApplicationCommand, IRequest<CommandResult>;