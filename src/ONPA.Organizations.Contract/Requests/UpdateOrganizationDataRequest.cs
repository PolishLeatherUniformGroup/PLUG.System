using Microsoft.AspNetCore.Mvc;

namespace ONPA.Organizations.Contract.Requests;

public sealed record UpdateOrganizationDataRequest([FromRoute]Guid OrganizationId,[FromBody]OrganizationData Data)
{
}