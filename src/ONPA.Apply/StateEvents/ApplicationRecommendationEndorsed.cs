using ONPA.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationRecommendationEndorsed :StateEventBase
{
    public Guid RecommendationId { get; private set; }

    public ApplicationRecommendationEndorsed(Guid recommendationId)
    {
        this.RecommendationId = recommendationId;
    }

    private ApplicationRecommendationEndorsed(Guid aggregateId, long aggregateVersion, Guid recommendationId) : base(aggregateId, aggregateVersion)
    {
        this.RecommendationId = recommendationId;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationRecommendationEndorsed(aggregateId, aggregateVersion, this.RecommendationId);
    }
}