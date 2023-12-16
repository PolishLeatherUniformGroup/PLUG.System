using Microsoft.AspNetCore.Mvc;
using ONPA.Membership.Contract.Requests.Dtos;

namespace ONPA.Membership.Contract.Requests;

public record MemberSuspensionAppealRequest([FromRoute] Guid MemberId, [FromBody] SuspensionAppeal Appeal);