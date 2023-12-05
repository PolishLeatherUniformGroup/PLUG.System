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

    private ApplicationNotRecommendedDomainEvent(Guid aggregateId, string firstName, string email) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.Email = email;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new ApplicationNotRecommendedDomainEvent(aggregateId,this.FirstName,this.Email);
    }
}