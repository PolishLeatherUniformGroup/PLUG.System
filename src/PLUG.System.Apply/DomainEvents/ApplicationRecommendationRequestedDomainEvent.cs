using PLUG.System.Common.Domain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationRecommendationRequestedDomainEvent : DomainEventBase
{
    public Guid RecommendingMemberId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public ApplicationRecommendationRequestedDomainEvent(Guid recommendingMemberId, string firstName, string lastName)
    {
        this.RecommendingMemberId = recommendingMemberId;
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    private ApplicationRecommendationRequestedDomainEvent(Guid aggregateId, Guid recommendingMemberId, string firstName,
        string lastName) : base(aggregateId)
    {
        this.RecommendingMemberId = recommendingMemberId;
        this.FirstName = firstName;
        this.LastName = lastName;
    }


    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new ApplicationRecommendationRequestedDomainEvent(aggregateId,
            this.RecommendingMemberId, this.FirstName,this.LastName);
    }
}