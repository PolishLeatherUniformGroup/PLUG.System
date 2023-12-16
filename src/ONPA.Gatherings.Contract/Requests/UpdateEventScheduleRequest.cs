using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Gatherings.Contract.Requests.Dtos;

namespace ONPA.Gatherings.Contract.Requests;

public record UpdateEventScheduleRequest([FromRoute]Guid EventId, [FromBody]UpdateEventSchedule Schedule): MultiTenantRequest;