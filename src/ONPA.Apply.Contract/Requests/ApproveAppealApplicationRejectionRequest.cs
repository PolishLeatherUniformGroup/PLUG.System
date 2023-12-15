using Microsoft.AspNetCore.Mvc;

namespace ONPA.Apply.Contract.Requests;

public record ApproveAppealApplicationRejectionRequest([FromRoute]Guid ApplicationId, [FromBody] ApproveAppeal Approval);