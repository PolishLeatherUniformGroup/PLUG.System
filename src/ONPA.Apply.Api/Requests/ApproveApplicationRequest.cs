using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Api.Requests;

public record ApproveApplicationRequest([FromRoute]Guid ApplicationId, [FromBody]ApplicationApproval Decision);