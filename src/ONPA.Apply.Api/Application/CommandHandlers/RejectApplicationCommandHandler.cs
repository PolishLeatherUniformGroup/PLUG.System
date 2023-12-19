using ONPA.Apply.Api.Application.Commands;
using PLUG.System.Apply.Domain;
using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;

namespace ONPA.Apply.Api.Application.CommandHandlers;

public sealed class RejectApplicationCommandHandler : MultiTenantApplicationCommandHandlerBase<RejectApplicationCommand>
{
    private readonly IMultiTenantAggregateRepository<ApplicationForm> _aggregateRepository;

    public RejectApplicationCommandHandler(IMultiTenantAggregateRepository<ApplicationForm> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(RejectApplicationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.TenantId, request.ApplicationId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }

            aggregate.RejectApplication(request.RejectionDate,request.DecisionDetail,request.DaysToAppeal);
            await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}