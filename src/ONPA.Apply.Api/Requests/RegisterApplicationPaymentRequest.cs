using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Api.Requests;

public record RegisterApplicationPaymentRequest([FromRoute]Guid ApplicationId, [FromBody] Payment Payment);