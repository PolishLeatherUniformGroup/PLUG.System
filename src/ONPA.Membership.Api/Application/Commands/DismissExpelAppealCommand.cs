﻿using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record DismissExpelAppealCommand(Guid TenantId, Guid MemberId, DateTime DecisionDate, string Justification): ApplicationCommandBase(TenantId);