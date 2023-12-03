using PLUG.System.Common.Domain;

namespace PLUG.System.Membership.Domain;

public sealed class GroupMember : Entity
{
    public CardNumber MemberNumber { get; private set; }
    public Guid MemberId { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; private set; }

    public DateTime JoinDate { get; private set; }
    public DateTime? EndDate { get; set; }

    public bool IsActive => !this.EndDate.HasValue || this.EndDate.Value.Date > DateTime.UtcNow.Date;

    public GroupMember(CardNumber memberNumber, Guid memberId, string firstName,
        string lastName,
        DateTime joinDate): base(Guid.NewGuid())
    {
        this.MemberNumber = memberNumber;
        this.MemberId = memberId;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.JoinDate = joinDate;
    }

    public void LeaveGroup(DateTime leaveDate)
    {
        this.EndDate = leaveDate;
    }
}