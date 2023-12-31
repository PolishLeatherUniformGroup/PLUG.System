﻿using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record RefundCancelledEnrollmentCommand(Guid TenantId, Guid EventId, Guid EnrollmentId, Money RefundedAmount, DateTime RefundDate) : ApplicationCommandBase(TenantId);