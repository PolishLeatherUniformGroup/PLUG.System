using ONPA.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationRecommendationRefused :StateEventBase
{
    public Guid RecommendationId { get; private set; }

    public ApplicationRecommendationRefused(Guid recommendationId)
    {
        this.RecommendationId = recommendationId;
    }

    private ApplicationRecommendationRefused(Guid tenantId, Guid aggregateId, long aggregateVersion, Guid recommendationId) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.RecommendationId = recommendationId;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationRecommendationRefused(tenantId, aggregateId, aggregateVersion, this.RecommendationId);
    }
}