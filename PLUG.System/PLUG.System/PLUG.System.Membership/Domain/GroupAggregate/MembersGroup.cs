using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.Domain;

public sealed class MembersGroup : AggregateRoot
{
    public string GroupName { get; private set; }
    public MembersGroupType GroupType { get; private set; }

    private readonly IList<GroupMember> _groupMembers = new List<GroupMember>();
    public IEnumerable<GroupMember> GroupMembers => this._groupMembers;

}