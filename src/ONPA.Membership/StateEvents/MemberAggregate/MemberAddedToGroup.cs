using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public  sealed class MemberAddedToGroup : StateEventBase
{
    public Guid GroupId { get; private set; }

    public MemberAddedToGroup(Guid groupId)
    {
        this.GroupId = groupId;
    }

    private MemberAddedToGroup(Guid tenantId, Guid aggregateId, long aggregateVersion, Guid groupId) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.GroupId = groupId;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new MemberAddedToGroup(tenantId, aggregateId, aggregateVersion, this.GroupId);
    }
}