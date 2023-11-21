using MediatR;
using PLUG.System.Common.Domain;

namespace PLUG.System.Common.Application;

public abstract class DomainEventHandlerBase<TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent :DomainEventBase
{
    public abstract Task Handle(TDomainEvent notification, CancellationToken cancellationToken);
    }