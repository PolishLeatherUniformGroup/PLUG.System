namespace ONPA.Membership.Api.Application.Services;

public interface IMemberRecommendationValidationService
{
    Task ValidateRecommendingMembers(Guid tenantId,Guid applicationId, params string [] memberNumbers);
}