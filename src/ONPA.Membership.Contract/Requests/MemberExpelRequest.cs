using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Membership.Contract.Requests.Dtos;

namespace ONPA.Membership.Contract.Requests;

public record MemberExpelRequest([FromRoute] Guid MemberId, [FromBody] MemberExpel Expel):MultiTenantRequest;