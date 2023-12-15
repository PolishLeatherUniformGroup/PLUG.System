using ONPA.Common.Application;
using ONPA.Organizations.Contract.Responses;

namespace ONPA.Organizations.Api.Application.Queries;

public sealed record GetOrganizationsQuery(int Page, int Limit) : ApplicationCollectionQueryBase<OrganizationResponse>(Guid.Empty,Page, Limit)
{
}