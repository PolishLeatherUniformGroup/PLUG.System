using MediatR;
using PLUG.System.Apply.Api.Application.Commands;
using PLUG.System.EventBus.Abstraction;
using PLUG.System.IntegrationEvents;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.Api.Application.IntegrationEvents.EventHandlers;

public class ApplicationRecommendersValidatedIntegrationEventHandler : IIntegrationEventHandler<ApplicationRecommendersValidatedIntegrationEvent>
{
    private readonly IMediator _mediator;

    public ApplicationRecommendersValidatedIntegrationEventHandler(IMediator mediator)
    {
        this._mediator = mediator;
    }

    public async Task Handle(ApplicationRecommendersValidatedIntegrationEvent @event)
    {
        Money yearlyFee;
        if (@event.YearFeeAmount.HasValue)
        {
            if (@event.FeeCurrency is null)
            {
                yearlyFee = new Money(@event.YearFeeAmount.Value);
            }
            else
            {
                yearlyFee = new Money(@event.YearFeeAmount.Value, @event.FeeCurrency);
            }
        }
        else
        {
            yearlyFee = new Money(0);
        }

        var command = new ValidateApplicationCommand(@event.ApplicationId, @event.Recommenders,yearlyFee);
        _ = await this._mediator.Send(command);
    }
}