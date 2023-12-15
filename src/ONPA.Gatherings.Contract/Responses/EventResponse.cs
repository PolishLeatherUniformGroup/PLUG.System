namespace ONPA.Gatherings.Contract.Responses;

public class EventResponse
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

    public int Status { get; set; }

    public EventResponse()
    {
        
    }

    public EventResponse(Guid id, string name, string description,
        string regulations,
        DateTime scheduledStart,
        int? plannedCapacity,
        int? availablePlaces,
        decimal pricePerPerson,
        string currency,
        DateTime publishDate,
        DateTime enrollmentDeadline,
        int status)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.Regulations = regulations;
        this.ScheduledStart = scheduledStart;
        this.PlannedCapacity = plannedCapacity;
        this.AvailablePlaces = availablePlaces;
        this.PricePerPerson = pricePerPerson;
        this.Currency = currency;
        this.PublishDate = publishDate;
        this.EnrollmentDeadline = enrollmentDeadline;
        this.Status = status;
    }
}