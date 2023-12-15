using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;

public sealed record CreateEventEnrollmentRequest([FromRoute]Guid EventId, [FromBody]CreateEnrollment Enrollment);