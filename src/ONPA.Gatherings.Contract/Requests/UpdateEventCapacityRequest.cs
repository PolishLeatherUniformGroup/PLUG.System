using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;

public record UpdateEventCapacityRequest([FromRoute]Guid EventId, [FromBody]UpdateEventCapacity Capacity);