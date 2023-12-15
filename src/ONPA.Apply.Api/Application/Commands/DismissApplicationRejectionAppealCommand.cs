using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record DismissApplicationRejectionAppealCommand
    (Guid TenantId,Guid ApplicationId,DateTime RejectDate, string DecisionDetail) : ApplicationCommandBase(TenantId);