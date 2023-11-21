using PLUG.System.Apply.Api.Application.Commands;
using PLUG.System.Apply.Domain;
using PLUG.System.Common.Application;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.Api.Application.CommandHandlers;

public class ValidateApplicationCommandHandler : ApplicationCommandHandlerBase<ValidateApplicationCommand>
{
    private readonly IAggregateRepository<ApplicationForm> _aggregateRepository;

    public ValidateApplicationCommandHandler(IAggregateRepository<ApplicationForm> aggregateRepository)
    {
        this._aggregateRepository = aggregateRepository;
    }

    public override async Task<CommandResult> Handle(ValidateApplicationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.ApplicationId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }

            if (request.Recommenders.Any(x => !x.MemberId.HasValue))
            {
                aggregate.CancelApplicationForm("Co najmniej jeden rekomedujący nie istnieje.");
            }
            else
            {
                aggregate.AcceptApplicationForm(this.CalculateRequiredFee(aggregate.ApplicationDate,request.YearlyFee));
                foreach (var recommender in request.Recommenders)
                {
                    aggregate.RequestRecommendation(recommender.MemberId!.Value, recommender.MemberNumber);
                }
            }
            await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }

    private Money CalculateRequiredFee(DateTime receivedDate, Money yearlyFee)
    {
        if (yearlyFee.Amount != 0)
        {
            var monthsToYearEnd = 12 - receivedDate.Month + 1;
            var monthlyFee = yearlyFee / 12;
            return monthlyFee * monthsToYearEnd;
        }

        return new Money(0);
    }
}