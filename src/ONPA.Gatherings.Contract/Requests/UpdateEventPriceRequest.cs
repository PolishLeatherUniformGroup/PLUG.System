using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;

public record UpdateEventPriceRequest([FromRoute]Guid EventId, [FromBody]UpdateEventPrice Price);