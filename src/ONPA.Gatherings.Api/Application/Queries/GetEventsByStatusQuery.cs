using System.Linq.Expressions;
using ONPA.Common.Application;
using ONPA.Gatherings.Contract.Responses;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Api.Application.Queries;

public sealed record GetEventsByStatusQuery : ApplicationCollectionQueryBase<EventResponse>
{
    public int Status { get; set; }

    public GetEventsByStatusQuery(Guid tenantId,
        int status,
        int page,
        int limit) : base(tenantId, page, limit)
    {
        Status = status;
    }

    public Expression<Func<Event, bool>> AsFilter()
    {
        return e => (int)e.Status == this.Status;
    }
}