using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record ApproveApplicationCommand(Guid ApplicationId,DateTime DecisionDate) : ApplicationCommandBase
{
    public Guid ApplicationId { get; private set; } = ApplicationId;
    public DateTime DecisionDate { get; private set; } = DecisionDate;
}