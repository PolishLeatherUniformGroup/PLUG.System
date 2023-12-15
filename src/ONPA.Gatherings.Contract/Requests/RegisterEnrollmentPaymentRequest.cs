using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;

public record RegisterEnrollmentPaymentRequest([FromRoute]Guid EventId, [FromRoute]Guid EnrollmentId,[FromBody]Payment Payment);