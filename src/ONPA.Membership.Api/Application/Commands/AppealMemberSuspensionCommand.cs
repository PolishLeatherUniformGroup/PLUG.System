﻿using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record AppealMemberSuspensionCommand(
    Guid TenantId,
    Guid MemberId,
    string Justification,
    DateTime AppealDate,
    string? Operator = null) : MultiTenantApplicationCommandBase(TenantId,
    Operator);