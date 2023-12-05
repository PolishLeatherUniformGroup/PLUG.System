using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record MakeMemberRegularCommand : ApplicationCommandBase
{
    public Guid MemberId { get; init; }

    public MakeMemberRegularCommand(Guid memberId)
    {
        this.MemberId = memberId;
    }
}