using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record RegisterMembershipFeePaymentRequest([FromRoute]Guid MemberId, [FromBody] Payment Payment);