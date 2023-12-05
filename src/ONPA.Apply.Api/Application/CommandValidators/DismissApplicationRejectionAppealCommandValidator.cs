using FluentValidation;
using ONPA.Apply.Api.Application.Commands;

namespace ONPA.Apply.Api.Application.CommandValidators;

public sealed class DismissApplicationRejectionAppealCommandValidator : AbstractValidator<DismissApplicationRejectionAppealCommand>
{
    public DismissApplicationRejectionAppealCommandValidator()
    {
        this.RuleFor(r => r.ApplicationId).NotEmpty();
        this.RuleFor(r => r.DecisionDetail).NotEmpty().MinimumLength(100);
        this.RuleFor(r => r.RejectDate.Date).LessThanOrEqualTo(DateTime.UtcNow.Date);
    }
}