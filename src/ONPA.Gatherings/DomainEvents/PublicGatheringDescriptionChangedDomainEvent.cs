using ONPA.Common.Domain;

namespace ONPA.Gatherings.DomainEvents;

public sealed class PublicGatheringDescriptionChangedDomainEvent :DomainEventBase
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Regulations { get; private set; }
    public List<string> Enrolled { get; private set; }

    public PublicGatheringDescriptionChangedDomainEvent(string name, string description, string regulations,
        List<string> enrolled)
    {
        this.Name = name;
        this.Description = description;
        this.Regulations = regulations;
        this.Enrolled = enrolled;
    }

    private PublicGatheringDescriptionChangedDomainEvent(Guid aggregateId, Guid tenantId, string name, string description,
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
        return new PublicGatheringDescriptionChangedDomainEvent(aggregateId,tenantId, this.Name, this.Description, this.Regulations, this.Enrolled);
    }
}