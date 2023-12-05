using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record RefuseApplicationRecommendationCommand(Guid ApplicationFormId,Guid RecommendationId, Guid RecommendingMemberId) : ApplicationCommandBase
{
    public Guid ApplicationFormId { get; private set; } = ApplicationFormId;
    public Guid RecommendingMemberId { get; private set; } = RecommendingMemberId;
    public Guid RecommendationId { get; init; } = RecommendationId;
}