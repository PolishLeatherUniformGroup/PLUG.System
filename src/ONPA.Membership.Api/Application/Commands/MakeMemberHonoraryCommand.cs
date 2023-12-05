using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record MakeMemberHonoraryCommand : ApplicationCommandBase
{
    public Guid MemberId { get; init; }

    public MakeMemberHonoraryCommand(Guid memberId)
    {
        this.MemberId = memberId;
    }
}