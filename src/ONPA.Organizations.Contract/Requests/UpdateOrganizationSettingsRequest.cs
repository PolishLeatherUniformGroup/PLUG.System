using Microsoft.AspNetCore.Mvc;

namespace ONPA.Organizations.Contract.Requests;

public sealed record UpdateOrganizationSettingsRequest([FromRoute]Guid OrganizationId,[FromBody]OrganizationSettings Settings)
{
}