using MediatR;

namespace ONPA.Common.Application;

public abstract record ApplicationQueryBase<TResult> : IRequest<TResult>
{
    public Guid TenantId { get; init; }
}
