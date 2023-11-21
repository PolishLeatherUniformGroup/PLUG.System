using FluentValidation;
using PLUG.System.Apply.Api.Application.Commands;

namespace PLUG.System.Apply.Api.Application.CommandValidators;

public sealed class DismissApplicationRejectionAppealCommandValidator : AbstractValidator<DismissApplicationRejectionAppealCommand>
{
    public DismissApplicationRejectionAppealCommandValidator()
    {
        this.RuleFor(r => r.ApplicationId).NotEmpty();
        this.RuleFor(r => r.DecisionDetail).NotEmpty().MinimumLength(100);
        this.RuleFor(r => r.RejectDate.Date).LessThanOrEqualTo(DateTime.UtcNow.Date);
    }
}