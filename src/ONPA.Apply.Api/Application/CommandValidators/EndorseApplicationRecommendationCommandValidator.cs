using FluentValidation;
using ONPA.Apply.Api.Application.Commands;

namespace ONPA.Apply.Api.Application.CommandValidators;

public sealed class EndorseApplicationRecommendationCommandValidator : AbstractValidator<EndorseApplicationRecommendationCommand>
{
    public EndorseApplicationRecommendationCommandValidator()
    {
        this.RuleFor(r => r.RecommendationId).NotEmpty();
        this.RuleFor(r => r.ApplicationFormId).NotEmpty();
        this.RuleFor(r => r.RecommendingMemberId).NotEmpty();
    }
}