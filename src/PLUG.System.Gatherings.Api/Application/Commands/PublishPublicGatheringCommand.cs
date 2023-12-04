using PLUG.System.Common.Application;

namespace PLUG.System.Gatherings.Api.Application.Commands;

public sealed record PublishPublicGatheringCommand : ApplicationCommandBase
{
    public Guid PublicGatheringId { get; init; }

    public PublishPublicGatheringCommand(Guid publicGatheringId)
    {
        this.PublicGatheringId = publicGatheringId;
    }
}