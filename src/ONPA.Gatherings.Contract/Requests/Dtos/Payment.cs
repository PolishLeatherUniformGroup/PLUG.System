namespace ONPA.Gatherings.Contract.Requests.Dtos;

public record Payment(DateTime PaidDate, decimal PaidAmount, string Currency);