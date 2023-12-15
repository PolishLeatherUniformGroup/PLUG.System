using ONPA.Common.Application;
using PLUG.System.SharedDomain;

namespace ONPA.Gatherings.Api.Application.Commands;

public sealed record ModifyEventPriceCommand(Guid TenantId, Guid PublicGatheringId, Money PricePerPerson) : ApplicationCommandBase(TenantId);