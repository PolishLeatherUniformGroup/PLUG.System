using Microsoft.AspNetCore.Mvc;

namespace ONPA.Gatherings.Contract.Requests;

public record UpdateEventScheduleRequest([FromRoute]Guid EventId, [FromBody]UpdateEventSchedule Schedule);