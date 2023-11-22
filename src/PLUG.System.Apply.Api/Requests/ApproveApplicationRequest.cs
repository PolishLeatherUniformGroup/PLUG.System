using Microsoft.AspNetCore.Mvc;

namespace PLUG.System.Apply.Api.Requests;

public record ApproveApplicationRequest([FromRoute]Guid ApplicationId, [FromBody]ApplicationApproval Decision);