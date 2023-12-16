using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Gatherings.Contract.Requests.Dtos;

namespace ONPA.Gatherings.Contract.Requests;

public record UpdateEventPriceRequest([FromRoute]Guid EventId, [FromBody]UpdateEventPrice Price): MultiTenantRequest;