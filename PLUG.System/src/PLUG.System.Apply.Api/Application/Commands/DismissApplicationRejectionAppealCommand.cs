using PLUG.System.Common.Application;

namespace PLUG.System.Apply.Api.Application.Commands;

public sealed record DismissApplicationRejectionAppealCommand
    (Guid ApplicationId,DateTime RejectDate, string DecisionDetail) : ApplicationCommandBase
{
    public Guid ApplicationId { get; init; } = ApplicationId;
    public DateTime RejectDate { get; init; } = RejectDate;
    public string DecisionDetail { get; init; } = DecisionDetail;
}