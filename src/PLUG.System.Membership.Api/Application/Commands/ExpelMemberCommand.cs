using PLUG.System.Common.Application;

namespace PLUG.System.Membership.Api.Application.Commands;

public sealed record ExpelMemberCommand : ApplicationCommandBase
{
    public Guid MemberId { get; init; }
    public string Justification { get; init; }
    public DateTime ExpelDate { get; init; }
    public int DaysToAppeal { get; init; }

    public ExpelMemberCommand(Guid memberId, string justification, DateTime suspendDate,
        int daysToAppeal)
    {
        this.MemberId = memberId;
        this.Justification = justification;
        this.ExpelDate = suspendDate;
        this.DaysToAppeal = daysToAppeal;
    }
}