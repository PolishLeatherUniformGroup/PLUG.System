using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Gatherings.Contract.Requests.Dtos;

namespace ONPA.Gatherings.Contract.Requests;

public record RegisterEnrollmentPaymentRequest(
    [FromRoute] Guid EventId,
    [FromRoute] Guid EnrollmentId,
    [FromBody] Payment Payment): MultiTenantRequest;