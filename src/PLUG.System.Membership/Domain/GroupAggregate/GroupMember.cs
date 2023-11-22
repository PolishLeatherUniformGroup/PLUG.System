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
}