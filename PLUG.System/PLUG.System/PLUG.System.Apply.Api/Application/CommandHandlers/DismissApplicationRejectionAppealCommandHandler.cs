using PLUG.System.Apply.Api.Application.Commands;
using PLUG.System.Apply.Domain;
using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;

namespace PLUG.System.Apply.Api.Application.CommandHandlers;

public sealed class DismissApplicationRejectionAppealCommandHandler : ApplicationCommandHandlerBase<DismissApplicationRejectionAppealCommand>
{
    private readonly IAggregateRepository<ApplicationForm> _aggregateRepository;

    public DismissApplicationRejectionAppealCommandHandler(IAggregateRepository<ApplicationForm> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(DismissApplicationRejectionAppealCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.ApplicationId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }

            aggregate.DismissAppeal(request.RejectDate,request.DecisionDetail);
            await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}