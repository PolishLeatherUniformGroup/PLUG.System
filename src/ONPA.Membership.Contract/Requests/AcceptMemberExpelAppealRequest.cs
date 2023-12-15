using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record AcceptMemberExpelAppealRequest([FromRoute] Guid MemberId, [FromBody] ExpelAppealDecision Decision);