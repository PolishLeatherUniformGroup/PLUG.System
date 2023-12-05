using FluentValidation;
using ONPA.Apply.Api.Application.Commands;

namespace ONPA.Apply.Api.Application.CommandValidators;

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