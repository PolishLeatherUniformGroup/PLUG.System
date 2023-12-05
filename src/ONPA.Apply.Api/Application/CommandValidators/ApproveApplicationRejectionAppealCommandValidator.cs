using FluentValidation;
using ONPA.Apply.Api.Application.Commands;

namespace ONPA.Apply.Api.Application.CommandValidators;

public sealed class ApproveApplicationRejectionAppealCommandValidator : AbstractValidator<ApproveApplicationRejectionAppealCommand>
{
    public ApproveApplicationRejectionAppealCommandValidator()
    {
        this.RuleFor(r => r.ApplicationId).NotEmpty();
        this.RuleFor(r => r.AcceptDate.Date).LessThanOrEqualTo(DateTime.UtcNow.Date);
    }
}