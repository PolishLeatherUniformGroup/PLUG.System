using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Gatherings.Api.Application.Commands;
using PLUG.System.Gatherings.Domain;

namespace PLUG.System.Gatherings.Api.Application.CommandHandlers;

public sealed class EnrollToPublicGatheringCommandHandler : ApplicationCommandHandlerBase<EnrollToPublicGatheringCommand>
{
    private readonly IAggregateRepository<PublicGathering> _aggregateRepository;

    public EnrollToPublicGatheringCommandHandler(IAggregateRepository<PublicGathering> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(EnrollToPublicGatheringCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.PublicGatheringId, cancellationToken);
            if (aggregate == null)
            {
                return new AggregateNotFoundException();
            }

            aggregate.Enroll(request.EnrollDate,request.Participants.Count,request.FirstName,request.LastName,request.Email,request.Participants);
            await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}