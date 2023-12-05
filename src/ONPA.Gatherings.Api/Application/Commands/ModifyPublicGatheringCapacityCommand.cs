using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ModifyPublicGatheringCapacityCommand : ApplicationCommandBase
{
    public Guid PublicGatheringId { get; init; }
    public int? PlannedCapacity { get; init; }
   
    public ModifyPublicGatheringCapacityCommand(Guid publicGatheringId, int? plannedCapacity)
    {
        this.PublicGatheringId = publicGatheringId;
        this.PlannedCapacity = plannedCapacity;
    }
}