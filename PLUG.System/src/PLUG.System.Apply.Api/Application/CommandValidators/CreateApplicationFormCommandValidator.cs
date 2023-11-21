using FluentValidation;
using PLUG.System.Apply.Api.Application.Commands;

namespace PLUG.System.Apply.Api.Application.CommandValidators;

public sealed class CreateApplicationFormCommandValidator : AbstractValidator<CreateApplicationFormCommand>
{
    public CreateApplicationFormCommandValidator()
    {
        this.RuleFor(r => r.FirstName).NotEmpty();
        this.RuleFor(r => r.LastName).NotEmpty();
        this.RuleFor(r => r.Email).NotEmpty().EmailAddress();
        this.RuleFor(r => r.Address).NotEmpty();
    }
}