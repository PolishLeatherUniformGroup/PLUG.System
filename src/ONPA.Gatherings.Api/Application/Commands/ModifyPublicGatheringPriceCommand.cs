using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Api.Application.Commands;

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