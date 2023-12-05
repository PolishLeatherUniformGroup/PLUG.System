using ONPA.Common.Application;
using ONPA.Membership.Domain;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record RemoveMemberFromGroupCommand : ApplicationCommandBase
{
    public Guid GroupId { get; init; }
    public CardNumber MemberNumber { get; init; }
    public DateTime RemoveDate { get; init; }

    public RemoveMemberFromGroupCommand(Guid groupId, CardNumber memberNumber, DateTime removeDate)
    {
        this.GroupId = groupId;
        this.MemberNumber = memberNumber;
        this.RemoveDate = removeDate;
    }
}