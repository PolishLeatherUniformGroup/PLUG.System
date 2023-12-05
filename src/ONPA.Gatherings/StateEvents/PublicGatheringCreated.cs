using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.StateEvents;

public sealed class PublicGatheringCreated : StateEventBase
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Regulations { get; private set; }
    public DateTime ScheduledStart { get; private set; }
    public int? PlannedCapacity { get; private set; }
    public Money PricePerPerson { get; private set; }
    public DateTime PublishDate { get; private set; }
    public DateTime EnrollmentDeadline { get; private set; }

    public PublicGatheringCreated(string name, string description, string regulations, DateTime scheduledStart, int? plannedCapacity, Money pricePerPerson, DateTime publishDate, DateTime enrollmentDeadline)
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

    private PublicGatheringCreated(Guid aggregateId, long aggregateVersion, string name, string description, string regulations, DateTime scheduledStart, int? plannedCapacity, Money pricePerPerson, DateTime publishDate, DateTime enrollmentDeadline) : base(aggregateId, aggregateVersion)
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

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new PublicGatheringCreated(aggregateId,
            aggregateVersion,
            this.Name,
            this.Description,
            this.Regulations,
            this.ScheduledStart,
            this.PlannedCapacity,
            this.PricePerPerson,
            this.PublishDate,
            this.EnrollmentDeadline);
    }
}