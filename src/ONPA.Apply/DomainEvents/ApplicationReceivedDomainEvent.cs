using ONPA.Common.Domain;

namespace PLUG.System.Apply.DomainEvents;

public sealed class ApplicationReceivedDomainEvent : DomainEventBase
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public List<string> Recommendations { get; private set; }


    public ApplicationReceivedDomainEvent(string firstName, string lastName, string email, List<string> recommendations)
    {
        this.FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        this.LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        this.Email = email ?? throw new ArgumentNullException(nameof(email));
        this.Recommendations = recommendations ?? throw new ArgumentNullException(nameof(recommendations));
    }

    private ApplicationReceivedDomainEvent(Guid aggregateId, string firstName, string lastName, string email, List<string> recommendations) : base(aggregateId)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Recommendations = recommendations;
    }

    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new ApplicationReceivedDomainEvent(aggregateId, this.FirstName, this.LastName, this.Email, this.Recommendations);
    }
}