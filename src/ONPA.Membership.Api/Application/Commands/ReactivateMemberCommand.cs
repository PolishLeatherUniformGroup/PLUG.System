using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record ReactivateMemberCommand : ApplicationCommandBase
{
    public Guid MemberId { get; init; }

    public ReactivateMemberCommand(Guid memberId)
    {
        this.MemberId = memberId;
    }
}