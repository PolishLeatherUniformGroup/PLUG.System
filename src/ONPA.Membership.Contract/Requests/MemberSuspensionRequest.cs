using Microsoft.AspNetCore.Mvc;

namespace ONPA.Membership.Contract.Requests;

public record MemberSuspensionRequest([FromRoute] Guid MemberId, [FromBody] MemberSuspension Suspension);