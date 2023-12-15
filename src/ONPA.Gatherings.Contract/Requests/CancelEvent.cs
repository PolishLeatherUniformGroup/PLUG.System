namespace ONPA.Gatherings.Contract.Requests;

public record CancelEvent(DateTime CancellationDate, decimal RefundableAmount, string Currency);