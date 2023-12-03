using PLUG.System.Common.Domain;
using PLUG.System.Membership.StateEvents;

namespace PLUG.System.Membership.Domain;

public partial class MembersGroup
{
    public void ApplyChange(GroupCreated change)
    {
        this.GroupName = change.GroupName;
        this.GroupType = change.GroupType;
    }

    public void ApplyChange(MemberJoinedGroup change)
    {
        this._groupMembers.Add(change.Member);
    }

    public void ApplyChange(MemberLeftGroup change)
    {
        var member = this._groupMembers.Single(x => x.Id == change.GroupMemberId);
        member.LeaveGroup(change.LeaveDate);
    }
}