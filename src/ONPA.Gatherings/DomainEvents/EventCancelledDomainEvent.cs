using ONPA.Gatherings.Domain;
using ONPA.Common.Domain;

namespace ONPA.Gatherings.DomainEvents;

public sealed class EventCancelledDomainEvent : DomainEventBase
{
    public string Name { get; private set; }
    public List<Participant> Participants { get; private set; }
    public string Reason { get; private set; }

    public EventCancelledDomainEvent(string name, List<Participant> participants, string reason)
    {
        this.Name = name;
        this.Participants = participants;
        this.Reason = reason;
    }

    private EventCancelledDomainEvent(Guid aggregateId, Guid tenantId, string name, List<Participant> participants, string reason) : base(aggregateId,tenantId)
    {
        this.Name = name;
        this.Participants = participants;
        this.Reason = reason;
    }
        
    public override IDomainEvent WithAggregate(Guid aggregateId,Guid tenantId)
    {
        return new EventCancelledDomainEvent(aggregateId,tenantId, this.Name, this.Participants,this.Reason);
    }
}