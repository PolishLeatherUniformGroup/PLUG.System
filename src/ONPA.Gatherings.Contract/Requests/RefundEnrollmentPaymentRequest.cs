using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;


public record RefundEnrollmentPaymentRequest([FromRoute]Guid EventId, [FromRoute]Guid EnrollmentId, [FromBody]Refund Refund);