using ONPA.EventBus.Events;

namespace ONPA.IntegrationEvents;

public record ApplicationRecommendersValidatedIntegrationEvent : IntegrationEvent
{
    public Guid ApplicationId { get; init; }
    public List<(string MemberNumber, Guid? MemberId)> Recommenders { get; init; }
    public decimal? YearFeeAmount { get; init; }
    public string? FeeCurrency { get; init; }

    public ApplicationRecommendersValidatedIntegrationEvent(Guid tenantId, Guid applicationId, List<(string MemberNumber, Guid? MemberId)> recommenders, decimal? yearFeeAmount, string? feeCurrency):base(tenantId)
    {
        this.ApplicationId = applicationId;
        this.Recommenders = recommenders;
        this.YearFeeAmount = yearFeeAmount;
        this.FeeCurrency = feeCurrency;
    }
}