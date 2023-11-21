using PLUG.System.Common.Application;

namespace PLUG.System.Apply.Api.Application.Commands;

public sealed record RejectApplicationCommand(Guid ApplicationId, string DecisionDetail, int DaysToAppeal) : ApplicationCommandBase
{
    public Guid ApplicationId { get; private set; } = ApplicationId;
    public string DecisionDetail { get; init; } = DecisionDetail;
    public int DaysToAppeal { get; init; } = DaysToAppeal;
}