using MediatR;
using PLUG.System.IntegrationEvents;
using PLUG.System.Membership.Api.Application.IntegrationEvents;
using PLUG.System.Membership.Api.Application.Queries;

namespace PLUG.System.Membership.Api.Application.Services;

public class MemberRecommendationValidationService : IMemberRecommendationValidationService
{
    private readonly IMediator _mediator;
    private readonly IIntegrationEventService _integrationEventService;

    public MemberRecommendationValidationService(IMediator mediator, IIntegrationEventService integrationEventService)
    {
        this._mediator = mediator;
        this._integrationEventService = integrationEventService;
    }

    public async Task ValidateRecommendingMembers(Guid applicationId, params string[] memberNumbers)
    {
        List<(string MemberNumber, Guid? MemberId)>
            validatedMembers = new List<(string MemberNumber, Guid? MemberId)>();
        foreach (var memberNumber in memberNumbers)
        {
            var query = new ValidateMemberNumbersQuery(memberNumber);
            var result = await this._mediator.Send(query);
            if (result.Value is not null)
            {
                validatedMembers.Add((result.Value.MemberNumber, result.Value.MemberId));
            }
            else
            {
                validatedMembers.Add((memberNumber,null));
            }
        }

        var fee = await this._mediator.Send(new GetYearlyFeeQuery(DateTime.UtcNow.Year));
        
        var integrationEvent = new ApplicationRecommendersValidatedIntegrationEvent(applicationId,
            validatedMembers, fee.Value.Amount, fee.Value.Currency);
        await this._integrationEventService.AddAndSaveEventAsync(integrationEvent);
    }
}