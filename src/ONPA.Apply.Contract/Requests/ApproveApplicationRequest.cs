using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Contract.Requests;

public record ApproveApplicationRequest([FromRoute]Guid ApplicationId, [FromBody]ApplicationApproval Decision);