using ONPA.Membership.Domain;
using ONPA.Common.Domain;

namespace ONPA.Membership.StateEvents;

public class GroupCreated : StateEventBase
{
    public string GroupName { get; private set; }
    public MembersGroupType GroupType { get; private set; }

    public GroupCreated(string groupName, MembersGroupType groupType)
    {
        this.GroupName = groupName;
        this.GroupType = groupType;
    }

    private GroupCreated(Guid tenantId, Guid aggregateId, long aggregateVersion, string groupName,
        MembersGroupType groupType) : base(tenantId, aggregateId, aggregateVersion)
    {
        this.GroupName = groupName;
        this.GroupType = groupType;
    }

    public override IStateEvent WithAggregate(Guid tenantId, Guid aggregateId, long aggregateVersion)
    {
        return new GroupCreated(tenantId,aggregateId, aggregateVersion, this.GroupName, this.GroupType);
    }
}