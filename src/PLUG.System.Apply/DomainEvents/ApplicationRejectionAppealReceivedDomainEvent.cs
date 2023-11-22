using PLUG.System.Common.Domain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationRejectionAppealReceivedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string Email { get; private set; }
    public DateTime AppealDate { get; private set; }

    public ApplicationRejectionAppealReceivedDomainEvent(string firstName, string email, DateTime appealDate)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.AppealDate = appealDate;
    }

    private ApplicationRejectionAppealReceivedDomainEvent(Guid aggregateId, string firstName, string email,
       DateTime appealDate) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.AppealDate = appealDate;

    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new ApplicationRejectionAppealReceivedDomainEvent(aggregateId, this.FirstName,this.Email,this.AppealDate);
    }
}