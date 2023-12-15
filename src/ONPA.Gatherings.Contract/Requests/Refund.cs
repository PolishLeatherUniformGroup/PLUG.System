namespace ONPA.Gatherings.Contract.Requests;

public record Refund(DateTime RefundDate, decimal RefundAmount, string Currency);