using Microsoft.AspNetCore.Mvc;
using ONPA.Apply.Contract.Requests.Dtos;
using ONPA.Common.Infrastructure;

namespace ONPA.Apply.Contract.Requests;

public record RegisterApplicationPaymentRequest([FromRoute]Guid ApplicationId, [FromBody] Payment Payment):MultiTenantRequest;