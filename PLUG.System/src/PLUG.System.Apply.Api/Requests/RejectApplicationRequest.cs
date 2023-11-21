using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api__OLD.Requests.Apply;

public record RejectApplicationRequest([FromRoute]Guid ApplicationId, [FromBody]ApplicationRejection Decision);