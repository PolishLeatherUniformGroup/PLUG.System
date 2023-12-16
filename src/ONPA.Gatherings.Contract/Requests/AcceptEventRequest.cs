using Microsoft.AspNetCore.Mvc;
using ONPA.Common.Infrastructure;

namespace ONPA.Gatherings.Contract.Requests;

public record AcceptEventRequest([FromRoute]Guid EventId) : MultiTenantRequest;