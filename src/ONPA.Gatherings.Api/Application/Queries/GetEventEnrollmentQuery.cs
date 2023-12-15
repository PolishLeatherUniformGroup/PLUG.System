using ONPA.Common.Application;
using ONPA.Gatherings.Contract.Responses;

namespace ONPA.Gatherings.Api.Application.Queries;

public sealed record GetEventEnrollmentQuery(Guid TenantId, Guid EventId, Guid EnrollmentId) : ApplicationQueryBase<EnrollmentResponse>(TenantId);