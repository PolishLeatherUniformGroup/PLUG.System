using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public record ApplicationAcknowledgedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public decimal RequiredFeeAmount { get; init; }
    public string FeeCurrency { get; init; }

    public ApplicationAcknowledgedIntegrationEvent(Guid tenantId,string firstName, string email, decimal requiredFeeAmount, string feeCurrency): base(tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RequiredFeeAmount = requiredFeeAmount;
        this.FeeCurrency = feeCurrency;
    }
}