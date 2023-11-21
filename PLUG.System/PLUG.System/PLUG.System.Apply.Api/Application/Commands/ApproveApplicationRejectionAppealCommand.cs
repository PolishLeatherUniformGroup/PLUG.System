using PLUG.System.Common.Application;

namespace PLUG.System.Apply.Api.Application.Commands;

public sealed record ApproveApplicationRejectionAppealCommand
    (Guid ApplicationId,DateTime AcceptDate) : ApplicationCommandBase
{
    public Guid ApplicationId { get; init; } = ApplicationId;
    public DateTime AcceptDate { get; init; } = AcceptDate;
}