using FluentValidation;
using PLUG.System.Apply.Api.Application.Commands;

namespace PLUG.System.Apply.Api.Application.CommandValidators;

public sealed class ApproveApplicationRejectionAppealCommandValidator : AbstractValidator<ApproveApplicationRejectionAppealCommand>
{
    public ApproveApplicationRejectionAppealCommandValidator()
    {
        this.RuleFor(r => r.ApplicationId).NotEmpty();
        this.RuleFor(r => r.AcceptDate.Date).LessThanOrEqualTo(DateTime.UtcNow.Date);
    }
}