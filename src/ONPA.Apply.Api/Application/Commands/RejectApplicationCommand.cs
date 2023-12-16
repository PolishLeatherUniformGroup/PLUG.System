using ONPA.Common.Application;

namespace ONPA.Apply.Api.Application.Commands;

public sealed record RejectApplicationCommand(Guid TenantId, Guid ApplicationId, DateTime RejectionDate, string DecisionDetail, int DaysToAppeal): ApplicationCommandBase(TenantId);