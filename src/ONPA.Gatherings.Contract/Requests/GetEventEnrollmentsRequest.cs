namespace ONPA.Gatherings.Contract.Requests;

public record GetEventEnrollmentsRequest(Guid EventId, int Page, int Limit)
{
    public string ToQueryString()
    {
        return $"{this.EventId}/enrollments?page={this.Page}&limit={this.Limit}";
    }
}