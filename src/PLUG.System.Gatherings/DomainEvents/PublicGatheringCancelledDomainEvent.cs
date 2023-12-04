using PLUG.System.Common.Domain;
using PLUG.System.Gatherings.Domain;

namespace PLUG.System.Gatherings.DomainEvents;

public sealed class PublicGatheringCancelledDomainEvent : DomainEventBase
{
    public string Name { get; private set; }
    public List<Participant> Participants { get; private set; }

    public PublicGatheringCancelledDomainEvent(string name, List<Participant> participants)
    {
        this.Name = name;
        this.Participants = participants;
    }

    private PublicGatheringCancelledDomainEvent(Guid aggregateId, string name, List<Participant> participants) : base(aggregateId)
    {
        this.Name = name;
        this.Participants = participants;
    }
        
    public override IDomainEvent WithAggregate(Guid aggregateId)
    {
        return new PublicGatheringCancelledDomainEvent(aggregateId, this.Name, this.Participants);
    }
}