using PLUG.System.Common.Application;

namespace PLUG.System.Membership.Api.Application.Commands;

public sealed record MakeMemberHonoraryCommand : ApplicationCommandBase
{
    public Guid MemberId { get; init; }

    public MakeMemberHonoraryCommand(Guid memberId)
    {
        this.MemberId = memberId;
    }
}