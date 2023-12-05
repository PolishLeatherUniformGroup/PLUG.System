using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record AppealApplicationRejectionCommand
    (Guid ApplicationId, string Justification, DateTime AppealReceived) : ApplicationCommandBase
{
    public string Justification { get; private set; } = Justification;
    public DateTime AppealReceived { get; private set; } = AppealReceived;
    public Guid ApplicationId { get; init; } = ApplicationId;
}
