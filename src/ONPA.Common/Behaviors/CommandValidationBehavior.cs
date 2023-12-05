using FluentValidation;
using MediatR;
using ONPA.Common.Application;

namespace ONPA.Common.Behaviors;

public class CommandValidationBehavior<TCommand,TResult> : IPipelineBehavior<TCommand, TResult>
    where TCommand : ApplicationCommandBase
    where TResult : CommandResult
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;

    public CommandValidationBehavior(IEnumerable<IValidator<TCommand>> validators)
    {
        this._validators = validators;
    }

    public async Task<TResult> Handle(TCommand request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        if (!this._validators.Any())
        {
            return await next();
        }

        string[] erorrs = this._validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(x => x is not null)
            .Select(e => e.ErrorMessage)
            .ToArray();
        if (erorrs.Any())
        {
            return (TResult)CommandResult.InValid(erorrs);
        }

        return await next();
    }
}