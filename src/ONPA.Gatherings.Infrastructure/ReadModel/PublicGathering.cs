namespace ONPA.Gatherings.Infrastructure.ReadModel;

public class PublicGathering
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Regulations { get; set; }
    public DateTime ScheduledStart { get; set; }

    public int? PlannedCapacity { get; set; }
    public int? AvailablePlaces { get; set; }

    public decimal PricePerPerson { get; set; }
    public string Currency { get; set; }

    public DateTime PublishDate { get; set; }
    public DateTime EnrollmentDeadline { get; set; }

    public GatheringStatus Status { get; set; }
}