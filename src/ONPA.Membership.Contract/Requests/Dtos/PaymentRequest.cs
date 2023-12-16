namespace ONPA.Membership.Contract.Requests.Dtos;

public record PaymentRequest(decimal Amount, string Currency, DateTime ValidToDate);