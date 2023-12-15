using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;

public record UpdateEventDescriptionRequest([FromRoute]Guid EventId, [FromBody]UpdateEventDescription Description);