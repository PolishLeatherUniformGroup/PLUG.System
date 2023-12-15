namespace ONPA.Membership.Contract.Requests;

public record Payment(decimal Amount, string Currency, DateTime PaymentDate);