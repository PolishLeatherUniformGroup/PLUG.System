using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Gatherings.Contract.Requests.Dtos;

namespace ONPA.Gatherings.Contract.Requests;

public record CancelEnrollmentRequest([FromRoute]Guid EventId, [FromRoute]Guid EnrollmentId, [FromBody]CancelEnrollment Cancellation): MultiTenantRequest;