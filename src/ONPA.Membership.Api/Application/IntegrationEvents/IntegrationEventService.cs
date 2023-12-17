using System.Diagnostics.CodeAnalysis;
using ONPA.EventBus.Abstraction;
using ONPA.EventBus.Events;
using ONPA.IntegrationEventsLog.Services;
using ONPA.Membership.Infrastructure.Database;

namespace ONPA.Membership.Api.Application.IntegrationEvents;

[ExcludeFromCodeCoverage(Justification = "Infrastructure Code")]
public class IntegrationEventService : IIntegrationEventService
{
    private readonly IEventBus _eventBus;
    private readonly MembershipContext _context;
    private readonly IIntegrationEventLogService _eventLogService;
    private readonly ILogger<IntegrationEventService> _logger;


    public IntegrationEventService(IEventBus eventBus, MembershipContext context, IIntegrationEventLogService eventLogService,
        ILogger<IntegrationEventService> logger)
    {
        this._eventBus = eventBus;
        this._context = context;
        this._eventLogService = eventLogService;
        this._logger = logger;
    }

    public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
    {
        var pendingLogEvents = await this._eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

        foreach (var logEvt in pendingLogEvents)
        {
            this._logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", logEvt.EventId, logEvt.IntegrationEvent);

            try
            {
                await this._eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                await this._eventBus.PublishAsync(logEvt.IntegrationEvent);
                await this._eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error publishing integration event: {IntegrationEventId}", logEvt.EventId);

                await this._eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
            }
        }
    }

    public async Task AddAndSaveEventAsync(IntegrationEvent evt)
    {
        this._logger.LogInformation("Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

        await this._eventLogService.SaveEventAsync(evt, this._context.GetCurrentTransaction());
    }
}