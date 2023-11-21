using PLUG.System.Common.Domain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationRecommendationRequestedDomainEvent : DomainEventBase
{
    public Guid RecommendationId { get; private set; }
    public Guid RecommendingMemberId { get; private set; }

    public ApplicationRecommendationRequestedDomainEvent(Guid recommendationId, Guid recommendingMemberId)
    {
        this.RecommendationId = recommendationId;
        this.RecommendingMemberId = recommendingMemberId;
    }

    private ApplicationRecommendationRequestedDomainEvent(Guid aggregateId, Guid recommendationId, Guid recommendingMemberId) : base(aggregateId)
    {
        this.RecommendationId = recommendationId;
        this.RecommendingMemberId = recommendingMemberId;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new ApplicationRecommendationRequestedDomainEvent(aggregateId, this.RecommendationId,
            this.RecommendingMemberId);
    }
}