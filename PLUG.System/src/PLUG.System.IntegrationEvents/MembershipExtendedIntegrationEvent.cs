using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEvents;

public record MembershipExtendedIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public DateTime ValidUntil { get; init; }

    public MembershipExtendedIntegrationEvent(string firstName, string email, DateTime validUntil)
    {
        FirstName = firstName;
        Email = email;
        ValidUntil = validUntil;
    }
}

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
        FirstName = firstName;
        Email = email;
        RequestedFeeAmount = requestedFeeAmount;
        RequestedFeeCurrency = requestedFeeCurrency;
        DueDate = dueDate;
        Period = period;
    }
}
public record MemberFeePaymentIncompleteIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public decimal RequestedFeeAmount { get; init; }
    public string RequestedFeeCurrency { get; init; }
    public decimal RegisteredFeeAmount { get; init; }
    public DateTime PaidDate { get; init; }
    public DateTime Period { get; init; }

    public MemberFeePaymentIncompleteIntegrationEvent(string firstName, string email, decimal requestedFeeAmount, string requestedFeeCurrency, decimal registeredFeeAmount, DateTime paidDate, DateTime period)
    {
        FirstName = firstName;
        Email = email;
        RequestedFeeAmount = requestedFeeAmount;
        RequestedFeeCurrency = requestedFeeCurrency;
        RegisteredFeeAmount = registeredFeeAmount;
        PaidDate = paidDate;
        Period = period;
    }
}