using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PLUG.System.Common.Domain;
using PLUG.System.Common.Infrastructure;

namespace PLUG.System.Membership.Infrastructure.Database;

public class MembershipContext :DbContext, IUnitOfWork
{
    public DbSet<StateEventLogEntry> EventsStream { get; set; }
    private IDbContextTransaction _currentTransaction;
    private readonly IMediator _mediator;

    public MembershipContext(DbContextOptions<MembershipContext> options, IMediator mediator) : base(options)
    {
        this._mediator = mediator;
    }

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
    {
        var stream = events.Select(e => new StateEventLogEntry(e).WithAggregate<TAggregate>());
        await this.EventsStream.AddRangeAsync(stream, cancellationToken);
    }

    protected async Task PublishDomainEvents(AggregateRoot aggregateRoot, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in aggregateRoot.GetDomainEvents())
        {
            await this._mediator.Publish(domainEvent, cancellationToken);
        }

        aggregateRoot.ClearDomainEvents();
    }

    public async Task<TAggregate?> ReadAggregate<TAggregate>(Guid aggregateId, CancellationToken cancellationToken)
    {
        var stream = await this.EventsStream.Where(e => e.AggregateId == aggregateId)
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
        var aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate), new object[] { aggregateId, events });
        return aggregate;
    }

    public async Task<TAggregate> StoreAggregate<TAggregate>(TAggregate aggregate, CancellationToken cancellationToken)
        where TAggregate : AggregateRoot
    {
        await this.AppendStream<TAggregate>(aggregate.GetStateEvents(), cancellationToken);
        await this.PublishDomainEvents(aggregate, cancellationToken);
        return aggregate;
    }
}