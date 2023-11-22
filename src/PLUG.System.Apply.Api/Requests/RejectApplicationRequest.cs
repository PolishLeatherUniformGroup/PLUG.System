using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api.Requests;

public record RejectApplicationRequest([FromRoute]Guid ApplicationId, [FromBody]ApplicationRejection Decision);