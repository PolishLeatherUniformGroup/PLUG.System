using Microsoft.AspNetCore.Mvc;
using ONPA.Apply.Contract.Requests.Dtos;
using ONPA.Common.Infrastructure;

namespace ONPA.Apply.Contract.Requests;

public record ApproveAppealApplicationRejectionRequest([FromRoute]Guid ApplicationId, [FromBody] ApproveAppeal Approval):MultiTenantRequest;