using MediatR;

namespace PLUG.System.Common.Application;

public abstract class ApplicationQueryHandlerBase<TQuery, TResult> : IRequestHandler<TQuery,TResult>
    where TQuery : ApplicationQueryBase<TResult>
{
    public abstract Task<TResult> Handle(TQuery request, CancellationToken cancellationToken);
  
}