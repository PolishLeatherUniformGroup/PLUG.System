using PLUG.System.Common.Application;

namespace PLUG.System.Membership.Api.Application.Commands;

public sealed record AppealMemberSuspensionCommand : ApplicationCommandBase
{
    public Guid MemberId { get; init; }
    public string Justification { get; init; }
    public DateTime AppealDate { get; init; }

    public AppealMemberSuspensionCommand(Guid memberId, string justification, DateTime appealDate)
    {
        this.MemberId = memberId;
        this.Justification = justification;
        this.AppealDate = appealDate;
    }
}