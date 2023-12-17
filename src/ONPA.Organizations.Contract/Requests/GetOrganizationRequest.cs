namespace ONPA.Organizations.Contract.Requests;

public record GetOrganizationRequest(Guid OrganizationId)
{
    public string ToQueryString()
    {
        return $"{this.OrganizationId}";
    }
}