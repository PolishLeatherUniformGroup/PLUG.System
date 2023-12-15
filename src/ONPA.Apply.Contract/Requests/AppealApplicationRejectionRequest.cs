using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Contract.Requests;

public record AppealApplicationRejectionRequest([FromRoute]Guid ApplicationId, [FromBody] Appeal Appeal);