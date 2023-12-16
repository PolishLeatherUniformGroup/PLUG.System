using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Gatherings.Contract.Requests.Dtos;

namespace ONPA.Gatherings.Contract.Requests;

public record UpdateEventCapacityRequest([FromRoute]Guid EventId, [FromBody]UpdateEventCapacity Capacity): MultiTenantRequest;