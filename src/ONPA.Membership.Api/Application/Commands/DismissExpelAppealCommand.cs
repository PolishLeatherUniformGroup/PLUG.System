using ONPA.Common.Application;

namespace ONPA.Membership.Api.Application.Commands;

public sealed record DismissExpelAppealCommand: ApplicationCommandBase
{
    public Guid MemberId { get; init; }
    public DateTime DecisionDate { get; init; }
    public string Justification { get; init; }

    public DismissExpelAppealCommand(Guid memberId, DateTime decisionDate, string justification)
    {
        this.MemberId = memberId;
        this.DecisionDate = decisionDate;
        this.Justification = justification;
    }
}