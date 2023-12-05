using ONPA.Common.Application;
using ONPA.Membership.Domain;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record AddMemberToGroupCommand : ApplicationCommandBase
{
    public Guid GroupId { get; init; }
    public CardNumber MemberNumber { get; init; }
    public Guid MemberId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime JoinDate { get; init; }

    public AddMemberToGroupCommand( Guid groupId,CardNumber memberNumber, Guid memberId, string firstName,
        string lastName,
        DateTime joinDate)
    {
        this.MemberNumber = memberNumber;
        this.MemberId = memberId;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.JoinDate = joinDate;
        this.GroupId = groupId;
    }
}