using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;
using ONPA.Common.Application;

namespace ONPA.Common.Behaviors;

[ExcludeFromCodeCoverage(Justification = "Tested via integration tests")]
public class CommandLoggingBehavior<TCommand,TResult> : IPipelineBehavior<TCommand,TResult>
    where TCommand : ApplicationCommandBase
    where TResult :CommandResult
{
    private readonly ILogger<CommandLoggingBehavior<TCommand,TResult>> _logger;

    public CommandLoggingBehavior(ILogger<CommandLoggingBehavior<TCommand,TResult>> logger)
    {
        this._logger = logger;
    }

    public async Task<TResult> Handle(TCommand request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
       this._logger.LogInformation($"Command {typeof(TCommand).Name} received.");
       var response = await next();
       if (response.IsSuccess)
       {
           this._logger.LogInformation($"Command {typeof(TCommand).Name} processed succesfully.");
       }

       if (!response.IsValid)
       {
           this._logger.LogInformation($"Command {typeof(TCommand).Name} validation failed.");
           this._logger.LogWarning($"Command Result: {JsonSerializer.Serialize(response)}");
       }
       else
       {
           this._logger.LogInformation($"Command {typeof(TCommand).Name} resulted in failure.");
           this._logger.LogWarning($"Command Result: {JsonSerializer.Serialize(response)}");
       }
       
       return response;
    }
    
}