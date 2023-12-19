using ONPA.Common.Domain;

namespace ONPA.Gatherings.StateEvents;

public sealed class EventDescriptionModified : StateEventBase
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Regulations { get; private set; }

    public EventDescriptionModified(string name, string description, string regulations)
    {
        this.Name = name;
        this.Description = description;
        this.Regulations = regulations;
    }

    private EventDescriptionModified(Guid tenantId, Guid aggregateId, long aggregateVersion, string name,
        string description,
        string regulations) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.Name = name;
        this.Description = description;
        this.Regulations = regulations;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new EventDescriptionModified(tenantId, aggregateId, aggregateVersion, this.Name, this.Description, this.Regulations);
    }
}