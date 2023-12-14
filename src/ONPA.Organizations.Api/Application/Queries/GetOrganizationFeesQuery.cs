using ONPA.Common.Application;
using ONPA.Organizations.Contract.Responses;

namespace ONPA.Organizations.Api.Application.Queries;

public sealed record GetOrganizationFeesQuery(Guid OrganizationId, int Page, int Limit) : ApplicationCollectionQueryBase<OrganizationResponse>(Page, Limit)
{
    public string ToQueryString()
    {
        return $"{OrganizationId}/fees?page={this.Page}&limit={this.Limit}";
    }
}