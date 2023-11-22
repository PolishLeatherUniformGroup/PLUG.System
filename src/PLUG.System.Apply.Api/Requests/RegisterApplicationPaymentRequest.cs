using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api.Requests;

public record RegisterApplicationPaymentRequest([FromRoute]Guid ApplicationId, [FromBody] Payment Payment);