using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using PLUG.System.Common.Domain;

namespace PLUG.System.Common.Infrastructure;

public sealed class StateEventLogEntry
{
    private static readonly JsonSerializerOptions s_indentedOptions = new() { WriteIndented = true };
    private static readonly JsonSerializerOptions s_caseInsensitiveOptions = new() { PropertyNameCaseInsensitive = true };

    private StateEventLogEntry() { }
    public StateEventLogEntry(IStateEvent @event)
    {
        EventId = @event.EventId;
        AggregateId = @event.AggregateId;
        CreationTime = @event.Timestamp;
        EventTypeName = @event.GetType().FullName!;
        Content = JsonSerializer.Serialize(@event, @event.GetType(), s_indentedOptions);
        StateEvent = @event;
    }
    [Required]
    public Guid EventId { get; private set; }
    [Required]
    public Guid AggregateId { get; private set; }
    
    [Required]
    public string AggregateTypeName { get; private set; }
    [Required]
    public string EventTypeName { get; private set; }
    [NotMapped]
    public string EventTypeShortName => EventTypeName.Split('.')?.Last()!;
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
        StateEvent = (JsonSerializer.Deserialize(this.Content, type, s_caseInsensitiveOptions) as IStateEvent)!;
        return this;
    }

    public  StateEventLogEntry WithAggregate<TAggregate>()
    {
        this.AggregateTypeName = typeof(TAggregate).FullName!;
        return this;
    }
    
    
}