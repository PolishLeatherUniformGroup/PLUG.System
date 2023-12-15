namespace ONPA.Gatherings.Contract.Responses;

public class EnrollmentResponse
{
    public EnrollmentResponse(Guid id, Guid eventId, DateTime registrationDate,
        int placesBooked,
        string firstName,
        string lastName,
        string email,
        string currency,
        decimal requiredPaymentAmount,
        decimal? paidAmount,
        DateTime? paidDate,
        DateTime? cancellationDate,
        decimal? refundableAmount,
        decimal? refundedAmount,
        DateTime? refundDate)
    {
        this.Id = id;
        this.EventId = eventId;
        this.RegistrationDate = registrationDate;
        this.PlacesBooked = placesBooked;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Currency = currency;
        this.RequiredPaymentAmount = requiredPaymentAmount;
        this.PaidAmount = paidAmount;
        this.PaidDate = paidDate;
        this.CancellationDate = cancellationDate;
        this.RefundableAmount = refundableAmount;
        this.RefundedAmount = refundedAmount;
        this.RefundDate = refundDate;
    }

    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int PlacesBooked { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Currency { get; set; }
    public decimal RequiredPaymentAmount { get; set; }
    public decimal? PaidAmount { get; set; }
    public DateTime? PaidDate { get; set; }

    public DateTime? CancellationDate { get; set; }
    public decimal? RefundableAmount { get; set; }
    public decimal? RefundedAmount { get; set; }
    public DateTime? RefundDate { get; set; }
    
    
}