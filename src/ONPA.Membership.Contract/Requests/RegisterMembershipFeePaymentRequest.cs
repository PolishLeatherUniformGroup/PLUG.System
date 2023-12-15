using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record RegisterMembershipFeePaymentRequest([FromRoute]Guid MemberId, [FromRoute] Guid FeeId, [FromBody] Payment Payment);