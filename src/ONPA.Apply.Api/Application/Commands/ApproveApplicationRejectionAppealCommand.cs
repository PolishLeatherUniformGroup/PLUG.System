using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record ApproveApplicationRejectionAppealCommand
    (Guid ApplicationId,DateTime AcceptDate) : ApplicationCommandBase
{
    public Guid ApplicationId { get; init; } = ApplicationId;
    public DateTime AcceptDate { get; init; } = AcceptDate;
}