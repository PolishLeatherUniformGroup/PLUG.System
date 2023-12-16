namespace ONPA.Gatherings.Contract.Requests.Dtos;

public record CancelEnrollment(DateTime CancellationDate, decimal RefundableAmount, string Currency);