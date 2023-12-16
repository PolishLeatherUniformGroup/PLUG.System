using Microsoft.AspNetCore.Mvc;
using ONPA.Apply.Contract.Requests.Dtos;
using ONPA.Common.Infrastructure;

namespace ONPA.Apply.Contract.Requests;

public record AppealApplicationRejectionRequest([FromRoute]Guid ApplicationId, [FromBody] Appeal Appeal) :MultiTenantRequest;