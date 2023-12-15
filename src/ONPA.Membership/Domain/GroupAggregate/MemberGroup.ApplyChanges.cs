using ONPA.Membership.StateEvents;

namespace ONPA.Membership.Domain;

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