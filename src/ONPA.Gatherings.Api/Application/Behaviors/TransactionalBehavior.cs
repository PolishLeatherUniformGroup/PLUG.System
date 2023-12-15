using MediatR;
using Microsoft.EntityFrameworkCore;
using ONPA.Common.Application;
using ONPA.Gatherings.Api.Application.IntegrationEvents;
using ONPA.Gatherings.Infrastructure.Database;

namespace ONPA.Gatherings.Api.Application.Behaviors;

public class TransactionalBehavior<TCommand, TResult> : IPipelineBehavior<TCommand, TResult>
    where TCommand : ApplicationCommandBase
    where TResult : CommandResult
{
    private readonly GatheringsContext _context;
    private readonly IIntegrationEventService _integrationEventService;
    private readonly ILogger<TransactionalBehavior<TCommand, TResult>> _logger;

    public TransactionalBehavior(GatheringsContext context, IIntegrationEventService integrationEventService, ILogger<TransactionalBehavior<TCommand, TResult>> logger)
    {
        this._context = context;
        this._integrationEventService = integrationEventService;
        this._logger = logger;
    }

    public async Task<TResult> Handle(TCommand request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        var response = default(TResult);
        var typeName = typeof(TCommand).Name;
        try
        {
            if (this._context.HasActiveTransaction)
            {
                return await next();
            }
            var strategy = this._context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                await using var transaction = await this._context.BeginTransactionAsync();
                using (this._logger.BeginScope(new List<KeyValuePair<string, object>> { new("TransactionContext", transaction.TransactionId) }))
                {
                    this._logger.LogInformation("Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                    response = await next();

                    if (response.IsSuccess)
                    {
                        this._logger.LogInformation("Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);
                        await this._context.CommitTransactionAsync(transaction);
                        transactionId = transaction.TransactionId;
                        await this._integrationEventService.PublishEventsThroughEventBusAsync(transactionId);
                    }
                    else
                    {
                        this._logger.LogInformation("Rollback transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);
                        this._context.RollbackTransaction();
                    }
                }

                
            });
            return response!;
        }
        catch (Exception ex)
        {
            if (this._context.HasActiveTransaction)
            {
                this._context.RollbackTransaction();
            }
            this._logger.LogError(ex, "Error Handling transaction for {CommandName} ({@Command})", typeName, request);
            throw;
        }
    }
}