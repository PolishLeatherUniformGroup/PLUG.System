using PLUG.System.Common.Application;

namespace PLUG.System.Membership.Api.Application.Commands;

public sealed record DismissSuspensionAppealCommand: ApplicationCommandBase
{
    public Guid MemberId { get; init; }
    public DateTime DecisionDate { get; init; }
    public string Justification { get; init; }

    public DismissSuspensionAppealCommand(Guid memberId, DateTime decisionDate, string justification)
    {
        this.MemberId = memberId;
        this.DecisionDate = decisionDate;
        this.Justification = justification;
    }
}