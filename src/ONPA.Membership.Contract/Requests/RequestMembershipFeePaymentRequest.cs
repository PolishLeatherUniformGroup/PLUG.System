using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record RequestMembershipFeePaymentRequest([FromRoute] Guid MemberId, [FromBody]PaymentRequest Request);