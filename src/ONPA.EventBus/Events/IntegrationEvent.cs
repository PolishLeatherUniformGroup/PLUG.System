using System.Text.Json.Serialization;

namespace ONPA.EventBus.Events;

public record IntegrationEvent
{
    public IntegrationEvent(Guid tenantId)
    {
        this.TenantId = tenantId;
        this.Id = Guid.NewGuid();
        this.CreationDate = DateTime.UtcNow;
    }

    [JsonInclude]
    public Guid Id { get; set; }
    
    [JsonInclude]
    public Guid TenantId { get; set; }

    [JsonInclude]
    public DateTime CreationDate { get; set; }
}