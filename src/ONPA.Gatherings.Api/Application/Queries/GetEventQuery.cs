using ONPA.Common.Application;
using ONPA.Gatherings.Contract.Responses;

namespace ONPA.Gatherings.Api.Application.Queries;

public sealed record GetEventQuery(Guid TenantId, Guid EventId) : ApplicationQueryBase<EventResponse>(TenantId);