using ONPA.Common.Application;
using ONPA.Organizations.Contract.Responses;

namespace ONPA.Organizations.Api.Application.Queries;

public sealed record GetOrganizationSettingsQuery(Guid OrganizationId) : ApplicationQueryBase<OrganizationSettingsResponse?>(OrganizationId)
{
}