using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api__OLD.Requests.Apply;

public record ApproveApplicationRequest([FromRoute]Guid ApplicationId, [FromBody]ApplicationApproval Decision);