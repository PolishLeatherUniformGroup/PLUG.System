using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using ONPA.EventBus.Events;

namespace ONPA.IntegrationEventsLog;

public class IntegrationEventLogEntry
{
    private static readonly JsonSerializerOptions s_indentedOptions = new() { WriteIndented = true };
    private static readonly JsonSerializerOptions s_caseInsensitiveOptions = new() { PropertyNameCaseInsensitive = true };

    private IntegrationEventLogEntry() { }
    public IntegrationEventLogEntry(IntegrationEvent @event, Guid transactionId)
    {
        this.EventId = @event.Id;
        this.CreationTime = @event.CreationDate;
        this.EventTypeName = @event.GetType().FullName;
        this.Content = JsonSerializer.Serialize(@event, @event.GetType(), s_indentedOptions);
        this.State = EventStateEnum.NotPublished;
        this.TimesSent = 0;
        this.TransactionId = transactionId;
    }
    public Guid EventId { get; private set; }
    [Required]
    public string EventTypeName { get; private set; }
    [NotMapped]
    public string EventTypeShortName => this.EventTypeName.Split('.')?.Last();
    [NotMapped]
    public IntegrationEvent IntegrationEvent { get; private set; }
    public EventStateEnum State { get; set; }
    public int TimesSent { get; set; }
    public DateTime CreationTime { get; private set; }
    [Required]
    public string Content { get; private set; }
    public Guid TransactionId { get; private set; }

    public IntegrationEventLogEntry DeserializeJsonContent(Type type)
    {
        this.IntegrationEvent = JsonSerializer.Deserialize(this.Content, type, s_caseInsensitiveOptions) as IntegrationEvent;
        return this;
    }
}