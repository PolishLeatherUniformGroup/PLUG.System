using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api.Requests;

public record AppealApplicationRejectionRequest([FromRoute]Guid ApplicationId, [FromBody] Appeal Appeal);