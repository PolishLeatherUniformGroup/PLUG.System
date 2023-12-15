using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;

public record CancelEnrollmentRequest([FromRoute]Guid EventId, [FromRoute]Guid EnrollmentId, [FromBody]CancelEnrollment Cancellation);