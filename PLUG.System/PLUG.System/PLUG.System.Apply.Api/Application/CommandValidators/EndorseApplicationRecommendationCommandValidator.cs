using FluentValidation;
using PLUG.System.Apply.Api.Application.Commands;

namespace PLUG.System.Apply.Api.Application.CommandValidators;

public sealed class EndorseApplicationRecommendationCommandValidator : AbstractValidator<EndorseApplicationRecommendationCommand>
{
    public EndorseApplicationRecommendationCommandValidator()
    {
        this.RuleFor(r => r.RecommendationId).NotEmpty();
        this.RuleFor(r => r.ApplicationFormId).NotEmpty();
        this.RuleFor(r => r.RecommendingMemberId).NotEmpty();
    }
}