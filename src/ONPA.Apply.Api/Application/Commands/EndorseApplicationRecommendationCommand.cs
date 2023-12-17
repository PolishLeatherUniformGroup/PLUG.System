using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record EndorseApplicationRecommendationCommand(Guid TenantId, Guid ApplicationFormId, Guid RecommendationId,Guid RecommendingMemberId) : ApplicationCommandBase(TenantId);