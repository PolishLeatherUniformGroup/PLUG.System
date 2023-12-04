using PLUG.System.Common.Application;

namespace PLUG.System.Gatherings.Api.Application.Commands;

public sealed record CancelPublicGatheringCommand : ApplicationCommandBase
{
    public Guid PublicGatheringId { get; init; }
    public DateTime CancellationDate { get; init; }
    public CancelPublicGatheringCommand(Guid publicGatheringId, DateTime cancellationDate)
    {
        this.PublicGatheringId = publicGatheringId;
        this.CancellationDate = cancellationDate;
    }   
}