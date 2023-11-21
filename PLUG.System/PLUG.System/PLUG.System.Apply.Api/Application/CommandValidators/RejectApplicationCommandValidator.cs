using FluentValidation;
using PLUG.System.Apply.Api.Application.Commands;

namespace PLUG.System.Apply.Api.Application.CommandValidators;

public sealed class RejectApplicationCommandValidator : AbstractValidator<RejectApplicationCommand>
{
    public RejectApplicationCommandValidator()
    {
        this.RuleFor(r => r.ApplicationId).NotEmpty();
        this.RuleFor(r => r.DecisionDetail).NotEmpty().MinimumLength(100);
        this.RuleFor(r => r.DaysToAppeal).NotEmpty().GreaterThan(0);
    }
}