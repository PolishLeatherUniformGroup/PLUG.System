using ONPA.Common.Domain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationFeeBalancedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public ApplicationFeeBalancedDomainEvent(string firstName, string email)
    {
        this.FirstName = firstName;
        this.Email = email;
       
    }

    private ApplicationFeeBalancedDomainEvent(Guid aggregateId,Guid tenantId, string firstName, string email) : base(aggregateId,tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new ApplicationFeeBalancedDomainEvent(aggregateId,tenantId,this.FirstName,this.Email);
    }
}