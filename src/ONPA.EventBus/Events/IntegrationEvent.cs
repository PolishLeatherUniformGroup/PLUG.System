using System.Text.Json.Serialization;

namespace ONPA.EventBus.Events;

public record IntegrationEvent
{
    public IntegrationEvent()
    {
        this.Id = Guid.NewGuid();
        this.CreationDate = DateTime.UtcNow;
    }

    [JsonInclude]
    public Guid Id { get; set; }

    [JsonInclude]
    public DateTime CreationDate { get; set; }
}