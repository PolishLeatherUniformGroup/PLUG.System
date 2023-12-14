using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public  sealed class MemberAddedToGroup : StateEventBase
{
    public Guid GroupId { get; private set; }

    public MemberAddedToGroup(Guid groupId)
    {
        this.GroupId = groupId;
    }

    private MemberAddedToGroup(Guid aggregateId, long aggregateVersion, Guid groupId) : base(aggregateId, aggregateVersion)
    {
        this.GroupId = groupId;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberAddedToGroup(aggregateId, aggregateVersion, this.GroupId);
    }
}