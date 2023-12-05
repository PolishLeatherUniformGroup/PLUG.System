using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Api.Requests;

public record AppealApplicationRejectionRequest([FromRoute]Guid ApplicationId, [FromBody] Appeal Appeal);