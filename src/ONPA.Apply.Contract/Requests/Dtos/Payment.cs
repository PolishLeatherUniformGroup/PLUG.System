namespace ONPA.Apply.Contract.Requests.Dtos;

public record Payment(decimal Amount, string Currency, DateTime PaymentDate);