using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Gatherings.Api.Application.Commands;
using ONPA.Gatherings.Domain;

namespace ONPA.Gatherings.Api.Application.CommandHandlers;

public sealed class CancelEventCommandHandler : ApplicationCommandHandlerBase<CancelEventCommand>
{
    private readonly IAggregateRepository<Event> _aggregateRepository;

    public CancelEventCommandHandler(IAggregateRepository<Event> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(CancelEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.EventId, cancellationToken);
            if (aggregate == null)
            {
                throw new AggregateNotFoundException();
            }
            aggregate.Cancel(request.CancellationDate, request.CancellationReason);
            await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}