using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ModifyPublicGatheringDescriptionCommand:ApplicationCommandBase
{
    public Guid PublicGatheringId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Regulations { get; init; }

    public ModifyPublicGatheringDescriptionCommand(Guid publicGatheringId, string name, string description, string regulations)
    {
        this.PublicGatheringId = publicGatheringId;
        this.Name = name;
        this.Description = description;
        this.Regulations = regulations;
    }
}