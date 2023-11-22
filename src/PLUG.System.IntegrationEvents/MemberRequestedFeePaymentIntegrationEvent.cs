using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEvents;

public record MemberRequestedFeePaymentIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public decimal RequestedFeeAmount { get; init; }
    public string RequestedFeeCurrency { get; init; }
    public DateTime DueDate { get; init; }
    public DateTime Period { get; init; }

    public MemberRequestedFeePaymentIntegrationEvent(string firstName, string email, decimal requestedFeeAmount, string requestedFeeCurrency, DateTime dueDate, DateTime period)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RequestedFeeAmount = requestedFeeAmount;
        this.RequestedFeeCurrency = requestedFeeCurrency;
        this.DueDate = dueDate;
        this.Period = period;
    }
}