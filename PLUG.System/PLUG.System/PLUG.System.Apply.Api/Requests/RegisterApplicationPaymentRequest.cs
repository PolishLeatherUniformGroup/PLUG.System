using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api__OLD.Requests.Apply;

public record RegisterApplicationPaymentRequest([FromRoute]Guid ApplicationId, [FromBody] Payment Payment);