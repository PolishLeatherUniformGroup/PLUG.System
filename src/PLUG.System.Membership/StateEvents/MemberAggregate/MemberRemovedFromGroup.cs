using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.StateEvents;

public  sealed class MemberRemovedFromGroup : StateEventBase
{
    public Guid GroupId { get; private set; }

    public MemberRemovedFromGroup(Guid groupId)
    {
        this.GroupId = groupId;
    }

    private MemberRemovedFromGroup(Guid aggregateId, long aggregateVersion, Guid groupId) : base(aggregateId, aggregateVersion)
    {
        this.GroupId = groupId;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new MemberRemovedFromGroup(aggregateId, aggregateVersion, this.GroupId);
    }
}