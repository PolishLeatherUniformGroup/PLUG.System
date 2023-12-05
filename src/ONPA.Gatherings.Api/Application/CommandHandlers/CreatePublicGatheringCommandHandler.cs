﻿using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Gatherings.Api.Application.Commands;
using ONPA.Gatherings.Domain;

namespace ONPA.Gatherings.Api.Application.CommandHandlers;

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
