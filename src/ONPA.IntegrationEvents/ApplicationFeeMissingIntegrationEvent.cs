using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public record ApplicationFeeMissingIntegrationEvent : IntegrationEvent
{
    public string FirstName { get; init; }
    public string Email { get; init; }
    public decimal RequiredFeeAmount { get; init; }
    public decimal RegisteredFeeAmount { get; init; }
    public string FeeCurrency { get; init; }

    public ApplicationFeeMissingIntegrationEvent(string firstName, string email, decimal requiredFeeAmount, decimal registeredFeeAmount, string feeCurrency)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RequiredFeeAmount = requiredFeeAmount;
        this.RegisteredFeeAmount = registeredFeeAmount;
        this.FeeCurrency = feeCurrency;
    }
}