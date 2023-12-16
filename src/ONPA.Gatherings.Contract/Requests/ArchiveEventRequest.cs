using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;

namespace ONPA.Gatherings.Contract.Requests;

public record ArchiveEventRequest([FromRoute]Guid EventId): MultiTenantRequest;