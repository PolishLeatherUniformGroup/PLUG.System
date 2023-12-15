using MediatR;

namespace ONPA.Common.Application;

public abstract record ApplicationCollectionQueryBase<TResult> : IRequest<CollectionResult<TResult>> where TResult : class
{
    public Guid TenantId { get; init; }
    public int Page { get; init; }
    public int Limit { get; init; }

    protected ApplicationCollectionQueryBase(Guid tenantId,int page, int limit)
    {
        this.TenantId = tenantId;
        this.Page = page;
        this.Limit = limit;
    }
}