namespace ONPA.Gatherings.Contract.Requests.Dtos;

public record Refund(DateTime RefundDate, decimal RefundAmount, string Currency);