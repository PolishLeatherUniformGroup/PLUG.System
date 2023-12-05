using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record CreatePublicGatheringCommand:ApplicationCommandBase
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string Regulations { get; init; }
    public DateTime ScheduledStart { get; init; }
    public int? PlannedCapacity { get; init; }
    public Money PricePerPerson { get; init; }
    public DateTime PublishDate { get; init; }
    public DateTime EnrollmentDeadline { get; init; }

    public CreatePublicGatheringCommand(string name, string description, string regulations, DateTime scheduledStart, int? plannedCapacity, Money pricePerPerson, DateTime publishDate, DateTime enrollmentDeadline)
    {
        this.Name = name;
        this.Description = description;
        this.Regulations = regulations;
        this.ScheduledStart = scheduledStart;
        this.PlannedCapacity = plannedCapacity;
        this.PricePerPerson = pricePerPerson;
        this.PublishDate = publishDate;
        this.EnrollmentDeadline = enrollmentDeadline;
    }
}