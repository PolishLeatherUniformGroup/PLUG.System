﻿using ONPA.Common.Infrastructure;

namespace ONPA.Gatherings.Contract.Requests;

public record GetEventRequest(Guid TenantId, Guid EventId): MultiTenantRequest;