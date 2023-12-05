using System.Linq.Expressions;
using MediatR;

namespace PLUG.System.Common.Application;

public abstract record ApplicationQueryBase<TResult> : IRequest<TResult>
{
}

public abstract record ApplicationCollectionQueryBase<TResult> : IRequest<CollectionResult<TResult>> where TResult : class
{
    public int Page { get; init; }
    public int Limit { get; init; }

    protected ApplicationCollectionQueryBase(int page, int limit)
    {
        this.Page = page;
        this.Limit = limit;
    }
}
