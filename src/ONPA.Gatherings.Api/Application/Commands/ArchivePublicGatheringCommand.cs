using ONPA.Common.Application;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ArchivePublicGatheringCommand : ApplicationCommandBase
{
    public Guid PublicGatheringId { get; init; }

    public ArchivePublicGatheringCommand(Guid publicGatheringId)
    {
        this.PublicGatheringId = publicGatheringId;
    }
}