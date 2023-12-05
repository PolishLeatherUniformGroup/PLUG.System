using FluentValidation;
using ONPA.Apply.Api.Application.Commands;

namespace ONPA.Apply.Api.Application.CommandValidators;

public sealed class AppealApplicationRejectionCommandValidator : AbstractValidator<AppealApplicationRejectionCommand>
{
    public AppealApplicationRejectionCommandValidator()
    {
        this.RuleFor(r => r.ApplicationId).NotEmpty();
        this.RuleFor(r => r.Justification).NotEmpty();
        this.RuleFor(r => r.AppealReceived.Date).LessThanOrEqualTo(DateTime.UtcNow.Date);
    }
}