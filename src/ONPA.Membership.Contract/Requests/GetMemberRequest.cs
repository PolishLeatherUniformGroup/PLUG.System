using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;

namespace ONPA.Membership.Contract.Requests;

public record GetMemberRequest([FromRoute] Guid MemberId):MultiTenantRequest;