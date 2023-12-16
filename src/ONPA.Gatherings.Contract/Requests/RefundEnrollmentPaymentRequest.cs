using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Gatherings.Contract.Requests.Dtos;

namespace ONPA.Gatherings.Contract.Requests;


public record RefundEnrollmentPaymentRequest(
    [FromRoute] Guid EventId,
    [FromRoute] Guid EnrollmentId,
    [FromBody] Refund Refund): MultiTenantRequest;