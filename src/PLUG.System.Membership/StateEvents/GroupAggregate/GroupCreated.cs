using PLUG.System.Common.Domain;
using PLUG.System.Membership.Domain;

namespace PLUG.System.Membership.StateEvents;

public class GroupCreated : StateEventBase
{
    public string GroupName { get; private set; }
    public MembersGroupType GroupType { get; private set; }

    public GroupCreated(string groupName, MembersGroupType groupType)
    {
        this.GroupName = groupName;
        this.GroupType = groupType;
    }

    private GroupCreated(Guid aggregateId, long aggregateVersion, string groupName,
        MembersGroupType groupType) : base(aggregateId, aggregateVersion)
    {
        this.GroupName = groupName;
        this.GroupType = groupType;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new GroupCreated(aggregateId, aggregateVersion, GroupName, GroupType);
    }
}