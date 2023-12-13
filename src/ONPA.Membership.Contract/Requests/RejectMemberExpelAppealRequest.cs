using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record RejectMemberExpelAppealRequest([FromRoute] Guid MemberId, [FromBody] ExpelAppealDecision Decision);