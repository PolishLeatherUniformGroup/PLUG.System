using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record AppealMemberExpelCommand : ApplicationCommandBase
{
    public Guid MemberId { get; init; }
    public string Justification { get; init; }
    public DateTime AppealDate { get; init; }

    public AppealMemberExpelCommand(Guid memberId, string justification, DateTime appealDate)
    {
        this.MemberId = memberId;
        this.Justification = justification;
        this.AppealDate = appealDate;
    }
}