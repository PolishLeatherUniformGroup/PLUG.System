using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api__OLD.Requests.Apply;

public record AppealApplicationRejectionRequest([FromRoute]Guid ApplicationId, [FromBody] Appeal Appeal);