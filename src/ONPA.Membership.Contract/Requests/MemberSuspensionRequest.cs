using Microsoft.AspNetCore.Mvc;
using ONPA.Membership.Contract.Requests.Dtos;

namespace ONPA.Membership.Contract.Requests;

public record MemberSuspensionRequest([FromRoute] Guid MemberId, [FromBody] MemberSuspension Suspension);