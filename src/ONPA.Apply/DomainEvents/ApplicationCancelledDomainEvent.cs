using ONPA.Common.Domain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationCancelledDomainEvent : DomainEventBase
{
    public ApplicationCancelledDomainEvent(string reason, string firstName, string email)
    {
        this.Reason = reason?? throw new ArgumentNullException(nameof(reason));
        this.FirstName = firstName;
        this.Email = email;
    }

    private ApplicationCancelledDomainEvent(Guid aggregateId,Guid tenantId, string reason, string firstName, string email) : base(aggregateId,tenantId)
    {
        this.Reason = reason ?? throw new ArgumentNullException(nameof(reason));
        this.FirstName = firstName;
        this.Email = email;
    }

    public string Reason { get; private set; }
    public string FirstName { get; private set; }
    public string Email { get; private set; }

    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new ApplicationCancelledDomainEvent(aggregateId,tenantId, this.Reason,this.FirstName,this.Email);
    }
}