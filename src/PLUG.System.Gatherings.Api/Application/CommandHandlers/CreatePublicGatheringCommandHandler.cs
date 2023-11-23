using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Gatherings.Api.Application.Commands;
using PLUG.System.Gatherings.Domain;

namespace PLUG.System.Gatherings.Api.Application.CommandHandlers;

public sealed class CreatePublicGatheringCommandHandler : ApplicationCommandHandlerBase<CreatePublicGatheringCommand>
{
    private readonly IAggregateRepository<PublicGathering> _aggregateRepository;

    public CreatePublicGatheringCommandHandler(IAggregateRepository<PublicGathering> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(CreatePublicGatheringCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = new PublicGathering(request.Name, request.Description, request.Regulations, request.ScheduledStart,
                request.PlannedCapacity, request.PricePerPerson,request.PublishDate,request.EnrollmentDeadline);
            await this._aggregateRepository.CreateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}