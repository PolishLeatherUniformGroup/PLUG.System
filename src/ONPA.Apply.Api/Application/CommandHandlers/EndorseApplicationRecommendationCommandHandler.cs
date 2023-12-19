using ONPA.Apply.Api.Application.Commands;
using PLUG.System.Apply.Domain;
using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;

namespace ONPA.Apply.Api.Application.CommandHandlers;

public sealed class EndorseApplicationRecommendationCommandHandler : MultiTenantApplicationCommandHandlerBase<EndorseApplicationRecommendationCommand>
{
    private readonly IMultiTenantAggregateRepository<ApplicationForm> _aggregateRepository;

    public EndorseApplicationRecommendationCommandHandler(IMultiTenantAggregateRepository<ApplicationForm> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(EndorseApplicationRecommendationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.TenantId,request.ApplicationFormId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }

            aggregate.EndorseRecommendation(request.RecommendationId,request.RecommendingMemberId);
            await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}