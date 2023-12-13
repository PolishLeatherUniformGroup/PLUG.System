using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record MemberExpelRequest([FromRoute] Guid MemberId, [FromBody] MemberExpel Expel);