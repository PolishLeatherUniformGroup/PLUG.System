namespace ONPA.Gatherings.Contract.Requests;

public record CancelEnrollment(DateTime CancellationDate, decimal RefundableAmount, string Currency);