using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Membership.Contract.Requests.Dtos;

namespace ONPA.Membership.Contract.Requests;

public record AcceptMemberSuspensionAppealRequest([FromRoute] Guid MemberId, [FromBody] SuspensionAppealDecision Decision):MultiTenantRequest;