using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Membership.Contract.Requests.Dtos;

namespace ONPA.Membership.Contract.Requests;

public record AcceptMemberExpelAppealRequest([FromRoute] Guid MemberId, [FromBody] ExpelAppealDecision Decision) :MultiTenantRequest;