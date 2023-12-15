namespace ONPA.Gatherings.Contract.Requests;

public record Payment(DateTime PaidDate, decimal PaidAmount, string Currency);