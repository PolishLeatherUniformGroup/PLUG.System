using ONPA.Common.Domain;

namespace ONPA.Gatherings.DomainEvents;

public sealed class EventDescriptionChangedDomainEvent :DomainEventBase
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Regulations { get; private set; }
    public List<string> Enrolled { get; private set; }

    public EventDescriptionChangedDomainEvent(string name, string description, string regulations,
        List<string> enrolled)
    {
        this.Name = name;
        this.Description = description;
        this.Regulations = regulations;
        this.Enrolled = enrolled;
    }

    private EventDescriptionChangedDomainEvent(Guid aggregateId, Guid tenantId, string name, string description,
        string regulations,
        List<string> enrolled) : base(aggregateId,tenantId)
    {
        this.Name = name;
        this.Description = description;
        this.Regulations = regulations;
        this.Enrolled = enrolled;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new EventDescriptionChangedDomainEvent(aggregateId,tenantId, this.Name, this.Description, this.Regulations, this.Enrolled);
    }
}