using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Api.Requests;

public record RejectApplicationRequest([FromRoute]Guid ApplicationId, [FromBody]ApplicationRejection Decision);