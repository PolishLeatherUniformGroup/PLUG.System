using MediatR;

namespace ONPA.Common.Application;

public abstract class ApplicationQueryHandlerBase<TQuery, TResult> : IRequestHandler<TQuery,TResult>
    where TQuery : ApplicationQueryBase<TResult>
{
    public abstract Task<TResult> Handle(TQuery request, CancellationToken cancellationToken);
  
}

public abstract class CollectionQueryHandlerBase<TQuery, TResult> : IRequestHandler<TQuery,CollectionResult<TResult>>
    where TQuery : ApplicationCollectionQueryBase<TResult>
    where TResult : class
{
    public abstract Task<CollectionResult<TResult>> Handle(TQuery request, CancellationToken cancellationToken);
  
}