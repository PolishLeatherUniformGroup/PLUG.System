using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Gatherings.Api.Application.Commands;
using PLUG.System.Gatherings.Domain;

namespace PLUG.System.Gatherings.Api.Application.CommandHandlers;

public sealed class ModifyPublicGatheringPriceCommandHandler : ApplicationCommandHandlerBase<ModifyPublicGatheringPriceCommand>
{
    private readonly IAggregateRepository<PublicGathering> _aggregateRepository;

    public ModifyPublicGatheringPriceCommandHandler(IAggregateRepository<PublicGathering> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(ModifyPublicGatheringPriceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.PublicGatheringId, cancellationToken);
            if (aggregate == null)
            {
                throw new AggregateNotFoundException();
            }
            aggregate.ModifyPrice(request.PricePerPerson);
            await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}