using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ONPA.Common.Domain;

namespace ONPA.Common.Infrastructure;

[ExcludeFromCodeCoverage(Justification = "Tested via integration tests")]
public sealed class StateEventLogEntry
{
    private static readonly JsonSerializerOptions s_indentedOptions = new() { WriteIndented = true };
    private static readonly JsonSerializerOptions s_caseInsensitiveOptions = new() { PropertyNameCaseInsensitive = true };

    private StateEventLogEntry() { }
    public StateEventLogEntry(IStateEvent @event)
    {
        this.EventId = @event.EventId;
        this.AggregateId = @event.AggregateId;
        this.CreationTime = @event.Timestamp;
        this.EventTypeName = @event.GetType().FullName!;
        this.Content = JsonSerializer.Serialize(@event, @event.GetType(), s_indentedOptions);
        this.StateEvent = @event;
        this.TenantId = @event.TenantId;
    }
    [Required]
    public Guid EventId { get; private set; }
    [Required]
    public Guid AggregateId { get; private set; }
    [Required]
    public Guid TenantId { get; private set; }
    
    [Required]
    public string AggregateTypeName { get; private set; }
    [Required]
    public string EventTypeName { get; private set; }
    [NotMapped]
    public string EventTypeShortName => this.EventTypeName.Split('.')?.Last()!;
    [NotMapped]
    public IStateEvent StateEvent { get; private set; }
    [Required]
    public DateTime CreationTime { get; private set; }
    [Required]
    public string Content { get; private set; }
 
    public StateEventLogEntry DeserializeJsonContent(Type? type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        this.StateEvent = (JsonSerializer.Deserialize(this.Content, type, s_caseInsensitiveOptions) as IStateEvent)!;
        return this;
    }

    public  StateEventLogEntry WithAggregate<TAggregate>()
    {
        this.AggregateTypeName = typeof(TAggregate).FullName!;
        return this;
    }
    
    
}