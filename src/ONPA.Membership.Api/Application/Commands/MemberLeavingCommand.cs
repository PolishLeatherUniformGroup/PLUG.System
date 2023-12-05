using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record MemberLeavingCommand :ApplicationCommandBase
{
    public Guid MemberId { get; init; }
    public DateTime LeaveDate { get; init; }

    public MemberLeavingCommand(Guid memberId, DateTime leaveDate)
    {
        this.MemberId = memberId;
        this.LeaveDate = leaveDate;
    }
}