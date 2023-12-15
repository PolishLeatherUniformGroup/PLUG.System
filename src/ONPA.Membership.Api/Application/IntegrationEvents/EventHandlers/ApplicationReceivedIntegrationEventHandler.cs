using ONPA.EventBus.Abstraction;
using ONPA.IntegrationEvents;
using ONPA.Membership.Api.Application.Services;

namespace ONPA.Membership.Api.Application.IntegrationEvents.EventHandlers;

public class ApplicationReceivedIntegrationEventHandler: IIntegrationEventHandler<ApplicationReceivedIntegrationEvent>
{
    private readonly IMemberRecommendationValidationService _memberRecommendationValidationService;

    public ApplicationReceivedIntegrationEventHandler(IMemberRecommendationValidationService memberRecommendationValidationService)
    {
        this._memberRecommendationValidationService = memberRecommendationValidationService;
    }

    public async Task Handle(ApplicationReceivedIntegrationEvent @event)
    {
        await this._memberRecommendationValidationService.ValidateRecommendingMembers(@event.TenantId,@event.ApplicationId,
            @event.Recommenders);
    }
}