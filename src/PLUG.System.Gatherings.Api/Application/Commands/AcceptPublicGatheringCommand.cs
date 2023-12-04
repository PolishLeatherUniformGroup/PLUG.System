using PLUG.System.Common.Application;

namespace PLUG.System.Gatherings.Api.Application.Commands;

public sealed record AcceptPublicGatheringCommand : ApplicationCommandBase
{
    public Guid PublicGatheringId { get; init; }

    public AcceptPublicGatheringCommand(Guid publicGatheringId)
    {
        this.PublicGatheringId = publicGatheringId;
    }
}