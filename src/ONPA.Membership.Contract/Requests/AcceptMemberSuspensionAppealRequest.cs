using Microsoft.AspNetCore.Mvc;
using ONPA.Membership.Contract.Requests.Dtos;

namespace ONPA.Membership.Contract.Requests;

public record AcceptMemberSuspensionAppealRequest([FromRoute] Guid MemberId, [FromBody] SuspensionAppealDecision Decision);