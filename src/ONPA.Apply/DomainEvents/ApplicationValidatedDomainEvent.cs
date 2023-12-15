using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationValidatedDomainEvent : DomainEventBase
{
    public Money RequiredFee { get; private set; }
    public string FirstName { get; private set; }
    public string Email { get; private set; }

    public ApplicationValidatedDomainEvent(Money requiredFee, string firstName, string email)
    {
        this.RequiredFee = requiredFee ?? throw new ArgumentNullException(nameof(requiredFee));
        this.FirstName = firstName;
        this.Email = email;
    }

    private ApplicationValidatedDomainEvent(Guid aggregateId,Guid tenantId, Money requiredFee, string firstName, string email) : base(aggregateId,tenantId)
    {
        this.RequiredFee = requiredFee ?? throw new ArgumentNullException(nameof(requiredFee));
        this.FirstName = firstName;
        this.Email = email;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new ApplicationValidatedDomainEvent(aggregateId,tenantId, this.RequiredFee, this.FirstName,this.Email);
    }
}