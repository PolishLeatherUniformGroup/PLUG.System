using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record MemberSuspensionAppealRequest([FromRoute] Guid MemberId, [FromBody] SuspensionAppeal Appeal);