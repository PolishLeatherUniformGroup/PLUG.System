namespace ONPA.Gatherings.Contract.Requests.Dtos;

public record CancelEvent(DateTime CancellationDate, string CancellationReason)
{
}