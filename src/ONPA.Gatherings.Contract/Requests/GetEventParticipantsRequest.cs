namespace ONPA.Gatherings.Contract.Requests;

public record GetEventParticipantsRequest(Guid EventId, int Page, int Limit)
{
    public string ToQueryString()
    {
        return $"{this.EventId}/participants?page={this.Page}&limit={this.Limit}";
    }
}