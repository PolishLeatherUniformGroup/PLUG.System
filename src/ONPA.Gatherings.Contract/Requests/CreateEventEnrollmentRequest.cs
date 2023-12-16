using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Gatherings.Contract.Requests.Dtos;

namespace ONPA.Gatherings.Contract.Requests;

public sealed record CreateEventEnrollmentRequest([FromRoute]Guid EventId, [FromBody]CreateEnrollment Enrollment): MultiTenantRequest;