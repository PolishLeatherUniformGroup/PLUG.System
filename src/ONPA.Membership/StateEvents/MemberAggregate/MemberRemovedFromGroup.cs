using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public  sealed class MemberRemovedFromGroup : StateEventBase
{
    public Guid GroupId { get; private set; }

    public MemberRemovedFromGroup(Guid groupId)
    {
        this.GroupId = groupId;
    }

    private MemberRemovedFromGroup(Guid tenantId, Guid aggregateId, long aggregateVersion, Guid groupId) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.GroupId = groupId;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MemberRemovedFromGroup(tenantId, aggregateId, aggregateVersion, this.GroupId);
    }
}