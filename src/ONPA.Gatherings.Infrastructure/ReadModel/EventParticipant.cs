namespace ONPA.Gatherings.Infrastructure.ReadModel;

public class EventParticipant
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Guid EnrollmentId { get; set; }
}