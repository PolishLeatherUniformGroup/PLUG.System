using ONPA.Common.Domain;

namespace PLUG.System.Apply.StateEvents;

public sealed class ApplicationRecommendationRequested :StateEventBase
{
    public ApplicationRecommendationRequested(Guid recommendationId, Guid recommendingMemberId, string recommendingMemberNumber, DateTime requestedDate)
    {
        this.RecommendationId = recommendationId;
        this.RecommendingMemberId = recommendingMemberId;
        this.RecommendingMemberNumber = recommendingMemberNumber;
        this.RequestedDate = requestedDate;
    }

    private ApplicationRecommendationRequested(Guid aggregateId, long aggregateVersion, Guid recommendationId, Guid recommendingMemberId, string recommendingMemberNumber, DateTime requestedDate) : base(aggregateId, aggregateVersion)
    {
        this.RecommendationId = recommendationId;
        this.RecommendingMemberId = recommendingMemberId;
        this.RecommendingMemberNumber = recommendingMemberNumber;
        this.RequestedDate = requestedDate;
    }

    public Guid RecommendationId { get; private set; }
    public Guid RecommendingMemberId { get; private set; }
    public string RecommendingMemberNumber { get; private set; }
    public DateTime RequestedDate { get; private set; }
    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new ApplicationRecommendationRequested(aggregateId, aggregateVersion, this.RecommendationId,
            this.RecommendingMemberId, this.RecommendingMemberNumber,this.RequestedDate);
    }
}