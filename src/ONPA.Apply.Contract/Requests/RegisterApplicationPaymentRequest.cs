using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Contract.Requests;

public record RegisterApplicationPaymentRequest([FromRoute]Guid ApplicationId, [FromBody] Payment Payment);