using MediatR;
using ONPA.Common.Domain;

namespace ONPA.Common.Application;

public abstract class DomainEventHandlerBase<TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent :DomainEventBase
{
    public abstract Task Handle(TDomainEvent notification, CancellationToken cancellationToken);
    }