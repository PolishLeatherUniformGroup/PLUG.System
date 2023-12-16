using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Membership.Contract.Requests.Dtos;

namespace ONPA.Membership.Contract.Requests;

public record RegisterMembershipFeePaymentRequest(
    [FromRoute] Guid MemberId,
    [FromRoute] Guid FeeId,
    [FromBody] Payment Payment) : MultiTenantRequest;