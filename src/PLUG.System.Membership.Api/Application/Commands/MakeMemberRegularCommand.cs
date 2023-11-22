using PLUG.System.Common.Application;

namespace PLUG.System.Membership.Api.Application.Commands;

public sealed record MakeMemberRegularCommand : ApplicationCommandBase
{
    public Guid MemberId { get; init; }

    public MakeMemberRegularCommand(Guid memberId)
    {
        this.MemberId = memberId;
    }
}