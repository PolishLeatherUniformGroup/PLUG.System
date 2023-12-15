using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Gatherings.Api.Application.Commands;
using ONPA.Gatherings.Domain;

namespace ONPA.Gatherings.Api.Application.CommandHandlers;

public sealed class CancelEnrollmentCommandHandler : ApplicationCommandHandlerBase<CancelEnrollmentCommand>
{
    private readonly IAggregateRepository<Event> _aggregateRepository;

    public CancelEnrollmentCommandHandler(IAggregateRepository<Event> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(CancelEnrollmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.PublicGatheringId, cancellationToken);
            if (aggregate == null)
            {
                return new AggregateNotFoundException();
            }

            aggregate.CancelEnrollment(request.EnrollmentId,request.CancellationDate,request.RefundableAmount);
            await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}