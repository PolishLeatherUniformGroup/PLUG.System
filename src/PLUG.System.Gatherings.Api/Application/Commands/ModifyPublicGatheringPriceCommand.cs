using PLUG.System.Common.Application;
using PLUG.System.SharedDomain;

namespace PLUG.System.Gatherings.Api.Application.Commands;

public sealed record ModifyPublicGatheringPriceCommand : ApplicationCommandBase
{
    public Guid PublicGatheringId { get; init; }
    public Money PricePerPerson { get; init; }
   
    public ModifyPublicGatheringPriceCommand(Guid publicGatheringId, Money pricePerPerson)
    {
        this.PublicGatheringId = publicGatheringId;
        this.PricePerPerson = pricePerPerson;
    }
}