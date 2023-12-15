using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;


public record RefundEnrollmentPayment([FromRoute]Guid EventId, [FromRoute]Guid EnrollmentId, [FromBody]Refund Refund);