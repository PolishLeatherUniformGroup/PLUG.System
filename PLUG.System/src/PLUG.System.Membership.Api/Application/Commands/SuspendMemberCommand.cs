using PLUG.System.Common.Application;

namespace PLUG.System.Membership.Api.Application.Commands;

public sealed record SuspendMemberCommand : ApplicationCommandBase
{
    public Guid MemberId { get; init; }
    public string Justification { get; init; }
    public DateTime SuspendDate { get; init; }
    public DateTime SuspendUntil { get; init; }
    public int DaysToAppeal { get; init; }

    public SuspendMemberCommand(Guid memberId, string justification, DateTime suspendDate,
        DateTime suspendUntil,
        int daysToAppeal)
    {
        this.MemberId = memberId;
        this.Justification = justification;
        this.SuspendDate = suspendDate;
        this.SuspendUntil = suspendUntil;
        this.DaysToAppeal = daysToAppeal;
    }
}