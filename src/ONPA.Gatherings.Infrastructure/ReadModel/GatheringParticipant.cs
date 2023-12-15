namespace ONPA.Gatherings.Infrastructure.ReadModel;

public class GatheringParticipant
{
    public Guid Id { get; set; }
    public Guid GatheringId { get; set; }
    public Guid? GatheringEnrollmentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}