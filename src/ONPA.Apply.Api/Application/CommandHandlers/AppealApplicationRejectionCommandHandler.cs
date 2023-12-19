using ONPA.Apply.Api.Application.Commands;
using PLUG.System.Apply.Domain;
using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;

namespace ONPA.Apply.Api.Application.CommandHandlers;

public sealed class AppealApplicationRejectionCommandHandler : MultiTenantApplicationCommandHandlerBase<AppealApplicationRejectionCommand>
{
    private readonly IMultiTenantAggregateRepository<ApplicationForm> _aggregateRepository;

    public AppealApplicationRejectionCommandHandler(IMultiTenantAggregateRepository<ApplicationForm> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(AppealApplicationRejectionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.TenantId, request.ApplicationId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }

            aggregate.AppealRejection(request.AppealReceived,request.Justification);
            await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}