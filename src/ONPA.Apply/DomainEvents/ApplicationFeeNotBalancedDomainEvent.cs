using ONPA.Common.Domain;
using PLUG.System.SharedDomain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationFeeNotBalancedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public Money RequiredPayment { get; private set; }
    public Money RegisteredPayment{ get; private set; }

    public ApplicationFeeNotBalancedDomainEvent(string firstName, string email, Money requiredPayment, Money registeredPayment)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RequiredPayment = requiredPayment;
        this.RegisteredPayment = registeredPayment;
    }

    private ApplicationFeeNotBalancedDomainEvent(Guid aggregateId,Guid tenantId, string firstName, string email, Money requiredPayment, Money registeredPayment) : base(aggregateId,tenantId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RequiredPayment = requiredPayment;
        this.RegisteredPayment = registeredPayment;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new ApplicationFeeNotBalancedDomainEvent(aggregateId,tenantId, this.FirstName,this.Email,this.RequiredPayment, this.RegisteredPayment);
    }
}