using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Contract.Requests;

public record RejectApplicationRequest([FromRoute]Guid ApplicationId, [FromBody]ApplicationRejection Decision);