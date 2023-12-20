namespace ONPA.Gatherings.Infrastructure.ReadModel;

public class EventEnrollment
{
    public Guid TenantId { get; set; }
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
    
    public List<EventParticipant> Participants { get; set; } = new();
}