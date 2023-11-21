using PLUG.System.Common.Domain;
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

    public ApplicationFeeNotBalancedDomainEvent(Guid aggregateId, string firstName, string email, Money requiredPayment, Money registeredPayment) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.RequiredPayment = requiredPayment;
        this.RegisteredPayment = registeredPayment;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new ApplicationFeeNotBalancedDomainEvent(aggregateId, this.FirstName,this.Email,this.RequiredPayment, this.RegisteredPayment);
    }
}