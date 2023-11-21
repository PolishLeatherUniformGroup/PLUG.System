using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEventsLog.Services;

public class IntegrationEventLogService<TContext> : IIntegrationEventLogService, IDisposable
    where TContext : DbContext
{
    private volatile bool _disposedValue;
    private readonly TContext _context;
    private readonly Type[] _eventTypes;

    public IntegrationEventLogService(TContext context)
    {
        this._context = context;
        this._eventTypes = Assembly.Load(Assembly.GetEntryAssembly().FullName)
            .GetTypes()
            .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
            .ToArray();
    }

    public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId)
    {
        var result = await this._context.Set<IntegrationEventLogEntry>()
            .Where(e => e.TransactionId == transactionId && e.State == EventStateEnum.NotPublished)
            .ToListAsync();

        if (result.Count != 0)
        {
            return result.OrderBy(o => o.CreationTime)
                .Select(e => e.DeserializeJsonContent(this._eventTypes.FirstOrDefault(t => t.Name == e.EventTypeShortName)));
        }

        return Array.Empty<IntegrationEventLogEntry>();
    }

    public Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));

        var eventLogEntry = new IntegrationEventLogEntry(@event, transaction.TransactionId);

        this._context.Database.UseTransaction(transaction.GetDbTransaction());
        this._context.Set<IntegrationEventLogEntry>().Add(eventLogEntry);

        return this._context.SaveChangesAsync();
    }

    public Task MarkEventAsPublishedAsync(Guid eventId)
    {
        return this.UpdateEventStatus(eventId, EventStateEnum.Published);
    }

    public Task MarkEventAsInProgressAsync(Guid eventId)
    {
        return this.UpdateEventStatus(eventId, EventStateEnum.InProgress);
    }

    public Task MarkEventAsFailedAsync(Guid eventId)
    {
        return this.UpdateEventStatus(eventId, EventStateEnum.PublishedFailed);
    }

    private Task UpdateEventStatus(Guid eventId, EventStateEnum status)
    {
        var eventLogEntry = this._context.Set<IntegrationEventLogEntry>().Single(ie => ie.EventId == eventId);
        eventLogEntry.State = status;

        if (status == EventStateEnum.InProgress)
            eventLogEntry.TimesSent++;

        return this._context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposedValue)
        {
            if (disposing)
            {
                this._context.Dispose();
            }


            this._disposedValue = true;
        }
    }

    public void Dispose()
    {
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}