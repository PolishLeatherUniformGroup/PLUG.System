using PLUG.System.EventBus.Events;

namespace PLUG.System.IntegrationEvents;

public record ApplicationRecommendersValidatedIntegrationEvent : IntegrationEvent
{
    public Guid ApplicationId { get; init; }
    public List<(string MemberNumber, Guid? MemberId)> Recommenders { get; init; }
    public decimal? YearFeeAmount { get; init; }
    public string? FeeCurrency { get; init; }

    public ApplicationRecommendersValidatedIntegrationEvent(Guid applicationId, List<(string MemberNumber, Guid? MemberId)> recommenders, decimal? yearFeeAmount, string? feeCurrency)
    {
        this.ApplicationId = applicationId;
        this.Recommenders = recommenders;
        this.YearFeeAmount = yearFeeAmount;
        this.FeeCurrency = feeCurrency;
    }
}