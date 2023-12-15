using ONPA.Common.Domain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationNotRecommendedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public ApplicationNotRecommendedDomainEvent(string firstName, string email)
    {
        this.FirstName = firstName;
        this.Email = email;
    }

    private ApplicationNotRecommendedDomainEvent(Guid aggregateId,Guid tenantId, string firstName, string email) : base(aggregateId,tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new ApplicationNotRecommendedDomainEvent(aggregateId,tenantId, this.FirstName,this.Email);
    }
}