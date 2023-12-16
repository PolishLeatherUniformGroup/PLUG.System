using Microsoft.AspNetCore.Mvc;
using ONPA.Apply.Contract.Requests.Dtos;
using ONPA.Common.Infrastructure;

namespace ONPA.Apply.Contract.Requests;

public record RejectAppealApplicationRejectionRequest([FromRoute]Guid ApplicationId, [FromBody] RejectAppeal Rejection):MultiTenantRequest;