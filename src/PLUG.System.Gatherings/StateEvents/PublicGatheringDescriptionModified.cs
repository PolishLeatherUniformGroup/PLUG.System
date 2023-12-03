using PLUG.System.Common.Domain;

namespace PLUG.System.Gatherings.StateEvents;

public sealed class PublicGatheringDescriptionModified : StateEventBase
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Regulations { get; private set; }

    public PublicGatheringDescriptionModified(string name, string description, string regulations)
    {
        this.Name = name;
        this.Description = description;
        this.Regulations = regulations;
    }

    private PublicGatheringDescriptionModified(Guid aggregateId, long aggregateVersion, string name,
        string description,
        string regulations) : base(aggregateId, aggregateVersion)
    {
        this.Name = name;
        this.Description = description;
        this.Regulations = regulations;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new PublicGatheringDescriptionModified(aggregateId, aggregateVersion, Name, Description,Regulations);
    }
}