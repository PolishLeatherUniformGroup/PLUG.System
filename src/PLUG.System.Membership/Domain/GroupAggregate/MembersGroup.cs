using PLUG.System.Common.Domain;
using PLUG.System.Common.Exceptions;
using PLUG.System.Membership.DomainEvents;
using PLUG.System.Membership.StateEvents;

namespace PLUG.System.Membership.Domain;

public sealed partial class MembersGroup : AggregateRoot
{
    public string GroupName { get; private set; }
    public MembersGroupType GroupType { get; private set; }

    private readonly IList<GroupMember> _groupMembers = new List<GroupMember>();
    public IEnumerable<GroupMember> GroupMembers => this._groupMembers;


    public MembersGroup(string groupName, MembersGroupType groupType)
    {
        this.GroupName = groupName;
        this.GroupType = groupType;

        var change = new GroupCreated(groupName, groupType);
        this.RaiseChangeEvent(change);
    }

    public void JoinGroup(CardNumber cardNumber, Guid memberId, string firstName, string lastName, DateTime joinDate)
    {
        if (this._groupMembers.Any(x => x.MemberId == memberId && x.IsActive))
        {
            throw new InvalidDomainOperationException();
        }

        var groupMember = new GroupMember(cardNumber, memberId, firstName, lastName, joinDate);
        this._groupMembers.Add(groupMember);

        var change = new MemberJoinedGroup(groupMember);
        this.RaiseChangeEvent(change);

        var domainEvent = new MemberJoinedGroupDomainEvent(memberId);
        this.RaiseDomainEvent(domainEvent);
    }
    
    public void RemoveFromGroup(CardNumber cardNumber, DateTime leaveDate)
    {
        var member = this._groupMembers.SingleOrDefault(x => x.MemberNumber == cardNumber && x.IsActive);
        if (member is null)
        {
            throw new EntityNotFoundException();
        }
        member.LeaveGroup(leaveDate);

        var change = new MemberLeftGroup(member.Id, leaveDate);
        this.RaiseChangeEvent(change);

        var domainEvent = new MemberLeftGroupDomainEvent(member.MemberId);
        this.RaiseDomainEvent(domainEvent);

    }
    
}