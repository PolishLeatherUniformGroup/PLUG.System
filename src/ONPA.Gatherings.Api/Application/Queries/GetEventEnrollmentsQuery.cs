using System.Linq.Expressions;
using ONPA.Common.Application;
using ONPA.Gatherings.Contract.Responses;
using ONPA.Gatherings.Infrastructure.ReadModel;

namespace ONPA.Gatherings.Api.Application.Queries;

public sealed record GetEventEnrollmentsQuery(
    Guid TenantId,
    Guid EventId,
    int Page,
    int Limit) : ApplicationCollectionQueryBase<EnrollmentResponse>(TenantId, Page, Limit)
{
    public Expression<Func<EventEnrollment,bool>> AsFilter()
    {
        return e => e.EventId == this.EventId;
    }
}