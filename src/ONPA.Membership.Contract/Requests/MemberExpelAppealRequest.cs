﻿using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Membership.Contract.Requests.Dtos;

namespace ONPA.Membership.Contract.Requests;

public record MemberExpelAppealRequest([FromRoute] Guid MemberId, [FromBody] ExpelAppeal Appeal):MultiTenantRequest;