using MediatR;
using PLUG.System.EventBus.Abstraction;
using PLUG.System.IntegrationEvents;
using PLUG.System.Membership.Api.Application.Services;

namespace PLUG.System.Membership.Api.Application.IntegrationEvents.EventHandlers;

public class ApplicationReceivedIntegrationEventHandler: IIntegrationEventHandler<ApplicationReceivedIntegrationEvent>
{
    private readonly IMemberRecommendationValidationService _memberRecommendationValidationService;

    public ApplicationReceivedIntegrationEventHandler(IMemberRecommendationValidationService memberRecommendationValidationService)
    {
        this._memberRecommendationValidationService = memberRecommendationValidationService;
    }

    public async Task Handle(ApplicationReceivedIntegrationEvent @event)
    {
        await this._memberRecommendationValidationService.ValidateRecommendingMembers(@event.ApplicationId,
            @event.Recommenders);
    }
}