using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;
using ONPA.Gatherings.Contract.Requests.Dtos;

namespace ONPA.Gatherings.Contract.Requests;

public record UpdateEventDescriptionRequest([FromRoute]Guid EventId, [FromBody]UpdateEventDescription Description): MultiTenantRequest;