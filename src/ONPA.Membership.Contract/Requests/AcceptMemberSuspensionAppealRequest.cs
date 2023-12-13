using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record AcceptMemberSuspensionAppealRequest([FromRoute] Guid MemberId, [FromBody] SuspensionAppealDecision Decision);