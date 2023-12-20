using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ONPA.Common.Domain;

namespace ONPA.Common.Infrastructure;

[ExcludeFromCodeCoverage(Justification = "Tested via integration tests")]
public class StreamContext : DbContext, IUnitOfWork
{
    public DbSet<StateEventLogEntry> EventsStream { get; set; }
    private IDbContextTransaction _currentTransaction;
    private readonly IMediator _mediator;

    protected StreamContext(IMediator? mediator)
    {
        this._mediator = mediator;
    }

    public StreamContext(DbContextOptions options, IMediator mediator) : base(options)
    {
        this._mediator = mediator;
    }
    
    public StreamContext(DbContextOptions options) : this(options,null){}


    public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
    {
        _ = await this.SaveChangesAsync(cancellationToken);
        return true;
    }

    public IDbContextTransaction GetCurrentTransaction() => this._currentTransaction;

    public bool HasActiveTransaction => this._currentTransaction != null;

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (this._currentTransaction != null)
        {
            return null;
        }

        this._currentTransaction = await this.Database.BeginTransactionAsync();

        return this._currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != this._currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await this.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            this.RollbackTransaction();
            throw;
        }
        finally
        {
            if (this._currentTransaction != null)
            {
                this._currentTransaction.Dispose();
                this._currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            this._currentTransaction?.Rollback();
        }
        finally
        {
            if (this._currentTransaction != null)
            {
                this._currentTransaction.Dispose();
                this._currentTransaction = null;
            }
        }
    }

    protected async Task AppendStream<TAggregate>(IEnumerable<IStateEvent> events, CancellationToken cancellationToken) 
        where TAggregate : MultiTenantAggregateRoot
    {
        var stream = events.Select(e => new StateEventLogEntry(e).WithAggregate<TAggregate>());
        await this.EventsStream.AddRangeAsync(stream, cancellationToken);
    }

    protected async Task PublishDomainEvents(MultiTenantAggregateRoot aggregateRoot, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in aggregateRoot.GetDomainEvents())
        {
            await this._mediator.Publish(domainEvent, cancellationToken);
        }

        aggregateRoot.ClearDomainEvents();
    }

    public async Task<TAggregate?> ReadAggregate<TAggregate>(Guid tenantId, Guid aggregateId, CancellationToken cancellationToken) 
        where TAggregate : MultiTenantAggregateRoot
    {
        var stream = await this.EventsStream.Where(e => e.AggregateId == aggregateId && e.TenantId == tenantId)
            .OrderBy(e => e.CreationTime)
            .ToListAsync(cancellationToken);
        if (!stream.Any())
        {
            return default;
        }

        var stateEvents = typeof(TAggregate).Assembly
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(IStateEvent)))
            .ToArray();
        var events = stream.OrderBy(x => x.CreationTime).Select(
            x => x.DeserializeJsonContent(stateEvents.FirstOrDefault(t => t.Name == x.EventTypeShortName)).StateEvent);
        var aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate), new object[] { tenantId, aggregateId, events });
        return aggregate;
    }

    public async Task<TAggregate> StoreAggregate<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken)
        where TAggregate : MultiTenantAggregateRoot
    {
        await this.AppendStream<TAggregate>(aggregate.GetStateEvents(), cancellationToken);
        await this.PublishDomainEvents(aggregate, cancellationToken);
        return aggregate;
    }

    
}