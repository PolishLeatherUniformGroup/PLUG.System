using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public record MemberFeePaymentIncompleteIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public decimal RequestedFeeAmount { get; init; }
    public string RequestedFeeCurrency { get; init; }
    public decimal RegisteredFeeAmount { get; init; }
    public DateTime PaidDate { get; init; }
    public DateTime Period { get; init; }

    public MemberFeePaymentIncompleteIntegrationEvent(Guid tenantId,string firstName, string email, decimal requestedFeeAmount, string requestedFeeCurrency, decimal registeredFeeAmount, DateTime paidDate, DateTime period): base(tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RequestedFeeAmount = requestedFeeAmount;
        this.RequestedFeeCurrency = requestedFeeCurrency;
        this.RegisteredFeeAmount = registeredFeeAmount;
        this.PaidDate = paidDate;
        this.Period = period;
    }
}