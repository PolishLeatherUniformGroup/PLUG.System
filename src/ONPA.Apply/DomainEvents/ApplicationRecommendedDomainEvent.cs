using ONPA.Common.Domain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationRecommendedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public ApplicationRecommendedDomainEvent(string firstName, string lastName)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    private ApplicationRecommendedDomainEvent(Guid aggregateId,Guid tenantId, string firstName, string lastName) : base(aggregateId,tenantId)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new ApplicationRecommendedDomainEvent(aggregateId,tenantId, this.FirstName,this.LastName);
    }
}