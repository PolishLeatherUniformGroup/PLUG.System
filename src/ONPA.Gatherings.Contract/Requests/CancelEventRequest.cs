using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Gatherings.Contract.Requests.Dtos;

namespace ONPA.Gatherings.Contract.Requests;

public record CancelEventRequest([FromRoute]Guid EventId, [FromBody]CancelEvent Cancellation): MultiTenantRequest;