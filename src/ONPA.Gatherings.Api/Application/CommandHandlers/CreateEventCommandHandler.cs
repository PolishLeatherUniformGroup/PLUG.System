using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Gatherings.Api.Application.Commands;
using ONPA.Gatherings.Domain;

namespace ONPA.Gatherings.Api.Application.CommandHandlers;

public sealed class CreateEventCommandHandler : MultiTenantApplicationCommandHandlerBase<CreateEventCommand>
{
    private readonly IMultiTenantAggregateRepository<Event> _aggregateRepository;

    public CreateEventCommandHandler(IMultiTenantAggregateRepository<Event> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = new Event(request.TenantId,request.Name, request.Description, request.Regulations, request.ScheduledStart,
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

