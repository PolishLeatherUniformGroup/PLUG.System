using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record MemberExpelAppealRequest([FromRoute] Guid MemberId, [FromBody] ExpelAppeal Appeal);