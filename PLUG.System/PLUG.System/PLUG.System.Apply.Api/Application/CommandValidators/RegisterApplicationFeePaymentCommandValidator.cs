using FluentValidation;
using PLUG.System.Apply.Api.Application.Commands;

namespace PLUG.System.Apply.Api.Application.CommandValidators;

public sealed class RegisterApplicationFeePaymentCommandValidator : AbstractValidator<RegisterApplicationFeePaymentCommand>
{
    public RegisterApplicationFeePaymentCommandValidator()
    {
        this.RuleFor(r => r.ApplicationId).NotEmpty();
        this.RuleFor(r => r.Paid).NotNull();
        this.RuleFor(r => r.Paid.Amount).GreaterThanOrEqualTo(0);
        this.RuleFor(r => r.Paid.Currency).NotEmpty();
    }
}