using MediatR;

namespace ONPA.Common.Application;

public abstract class CollectionQueryHandlerBase<TQuery, TResult> : IRequestHandler<TQuery,CollectionResult<TResult>>
    where TQuery : ApplicationCollectionQueryBase<TResult>
    where TResult : class
{
    public abstract Task<CollectionResult<TResult>> Handle(TQuery request, CancellationToken cancellationToken);
  
}