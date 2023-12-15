using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Contract.Requests;

public record RejectAppealApplicationRejectionRequest([FromRoute]Guid ApplicationId, [FromBody] RejectAppeal Rejection);