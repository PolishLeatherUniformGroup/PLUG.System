using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record ApproveApplicationRejectionAppealCommand
    (Guid TenantId,Guid ApplicationId,DateTime AcceptDate, string Justification) : ApplicationCommandBase(TenantId);