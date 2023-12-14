using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public sealed record AllMembershipFeeRequestedIntegrationEvent :IntegrationEvent
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }
    public DateTime DueDate { get; init; }
    public DateTime Period { get; init; }

    public AllMembershipFeeRequestedIntegrationEvent(Guid tenantId,
        decimal amount,
        string currency,
        DateTime dueDate,
        DateTime period) : base(tenantId)
    {
        this.Amount = amount;
        this.Currency = currency;
        this.DueDate = dueDate;
        this.Period = period;
    }
}