using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ONPA.IntegrationEventsLog.Utilities;

[ExcludeFromCodeCoverage(Justification = "Tested in integration tests")]
public class ResilientTransaction
{
    private readonly DbContext _context;
    private ResilientTransaction(DbContext context) => this._context = context ?? throw new ArgumentNullException(nameof(context));

    public static ResilientTransaction New(DbContext context) => new(context);

    public async Task ExecuteAsync(Func<Task> action)
    {
        //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
        //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
        var strategy = this._context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await this._context.Database.BeginTransactionAsync();
            await action();
            await transaction.CommitAsync();
        });
    }
}