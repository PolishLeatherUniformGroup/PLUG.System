namespace ONPA.Membership.Contract.Requests.Dtos;

public record Payment(decimal Amount, string Currency, DateTime PaymentDate);