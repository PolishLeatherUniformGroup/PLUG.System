using Microsoft.AspNetCore.Mvc;

namespace ONPA.Organizations.Contract.Requests;

public record GetOrganizationFeeForYearRequest([FromRoute] Guid OrganizationId, [FromRoute] int Year)
{
    public string ToQueryString()
    {
        var queryString = $"{this.OrganizationId}/fees/{this.Year}";
        return queryString;
    }

}