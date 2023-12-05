namespace ONPA.Membership.Api.Application.Services;

public interface IMemberRecommendationValidationService
{
    Task ValidateRecommendingMembers(Guid applicationId, params string [] memberNumbers);
}