using ONPA.Common.Application;
using ONPA.Organizations.Contract.Responses;

namespace ONPA.Organizations.Api.Application.Queries;

public sealed record GetOrganizationFeesQuery(Guid TenantId,Guid OrganizationId, int Page, int Limit) : ApplicationCollectionQueryBase<OrganizationResponse>(TenantId,Page, Limit)
{
    public string ToQueryString()
    {
        return $"{this.OrganizationId}/fees?page={this.Page}&limit={this.Limit}";
    }
}