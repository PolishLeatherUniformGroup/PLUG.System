using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;

public record CancelEventRequest([FromRoute]Guid EventId, [FromBody]CancelEvent Cancellation);