namespace ONPA.Membership.Contract.Requests;

public record PaymentRequest(decimal Amount, string Currency, DateTime ValidToDate);