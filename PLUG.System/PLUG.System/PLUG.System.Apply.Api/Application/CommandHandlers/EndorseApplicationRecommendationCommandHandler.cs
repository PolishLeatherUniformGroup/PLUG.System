using PLUG.System.Apply.Api.Application.Commands;
using PLUG.System.Apply.Domain;
using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;

namespace PLUG.System.Apply.Api.Application.CommandHandlers;

public sealed class EndorseApplicationRecommendationCommandHandler : ApplicationCommandHandlerBase<EndorseApplicationRecommendationCommand>
{
    private readonly IAggregateRepository<ApplicationForm> _aggregateRepository;

    public EndorseApplicationRecommendationCommandHandler(IAggregateRepository<ApplicationForm> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(EndorseApplicationRecommendationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.ApplicationFormId, cancellationToken);
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